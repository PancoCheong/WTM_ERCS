﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

using WalkingTec.Mvvm.Core;

namespace ERCS.ViewModel.HomeVMs
{
    public class LoginVM : BaseVM
    {
        [Required(AllowEmptyStrings = false)]
        [StringLength(50)]
        public string ITCode { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Password { get; set; }

        public bool RememberLogin { get; set; }

        public string Redirect { get; set; }

        public string VerifyCode { get; set; }

        public LoginUserInfo DoLogin(bool ignorePris = false)
        {
            var user = DC.Set<FrameworkUserBase>()
                .Include(x => x.UserRoles).Include(x => x.UserGroups)
                .Where(x => x.ITCode.ToLower() == ITCode.ToLower() && x.Password == Utils.GetMD5String(Password) && x.IsValid)
                .SingleOrDefault();

            if (user == null)
            {
                MSD.AddModelError("", Localizer["LoginFail"]);
                return null;
            }
            var roleIDs = user.UserRoles.Select(x => x.RoleId).ToList();
            var groupIDs = user.UserGroups.Select(x => x.GroupId).ToList();
            var dpris = DC.Set<DataPrivilege>()
                .Where(x => x.UserId == user.ID || (x.GroupId != null && groupIDs.Contains(x.GroupId.Value)))
                .Distinct()
                .ToList();
            ProcessTreeDp(dpris);
            LoginUserInfo rv = new LoginUserInfo
            {
                Id = user.ID,
                ITCode = user.ITCode,
                Name = user.Name,
                PhotoId = user.PhotoId,
                Roles = DC.Set<FrameworkRole>().Where(x => user.UserRoles.Select(y => y.RoleId).Contains(x.ID)).ToList(),
                Groups = DC.Set<FrameworkGroup>().Where(x => user.UserGroups.Select(y => y.GroupId).Contains(x.ID)).ToList(),
                DataPrivileges = dpris
            };
            if (ignorePris == false)
            {
                var pris = DC.Set<FunctionPrivilege>()
                    .Where(x => x.UserId == user.ID || (x.RoleId != null && roleIDs.Contains(x.RoleId.Value)))
                    .Distinct()
                    .ToList();
                rv.FunctionPrivileges = pris;
            }
            return rv;
        }


        private void ProcessTreeDp(List<DataPrivilege> dps)
        {
            var dpsSetting = GlobalServices.GetService<Configs>().DataPrivilegeSettings;
            foreach (var ds in dpsSetting)
            {
                if (typeof(ITreeData).IsAssignableFrom(ds.ModelType))
                {
                    var ids = dps.Where(x => x.TableName == ds.ModelName).Select(x => x.RelateId).ToList();
                    if (ids.Count > 0 && ids.Contains(null) == false)
                    {
                        List<Guid> tempids = new List<Guid>();
                        foreach (var item in ids)
                        {
                            if(Guid.TryParse(item,out Guid g))
                            {
                                tempids.Add(g);
                            }
                        }
                        List<Guid> subids = new List<Guid>();
                        subids.AddRange(GetSubIds(tempids.ToList(), ds.ModelType));
                        subids = subids.Distinct().ToList();
                        subids.ForEach(x => dps.Add(new DataPrivilege {
                          TableName = ds.ModelName,
                          RelateId = x.ToString()
                        }));
                    }
                }
            }
        }

        private IEnumerable<Guid> GetSubIds(List<Guid> p_id,Type modelType)
        {
            var basequery = DC.GetType().GetTypeInfo().GetMethod("Set").MakeGenericMethod(modelType).Invoke(DC, null) as IQueryable;
            var subids = basequery.Cast<ITreeData>().Where(x => p_id.Contains(x.ParentId.Value)).Select(x => x.ID).ToList();
            if (subids.Count > 0)
            {
                return subids.Concat(GetSubIds(subids, modelType));
            }
            else
            {
                return new List<Guid>();
            }
        }
    }

}
