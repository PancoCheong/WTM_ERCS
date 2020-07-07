﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using WalkingTec.Mvvm.Core;

namespace ERCS.ViewModel.HomeVMs
{
    public class ChangePasswordVM : BaseVM
    {
        [Display(Name = "Account")]
        public string ITCode { get; set; }

        [Display(Name = "OldPassword")]
        [Required(AllowEmptyStrings = false)]
        [StringLength(50, ErrorMessage = "{0}stringmax{1}")]
        public string OldPassword { get; set; }

        [Display(Name = "NewPassword")]
        [Required(AllowEmptyStrings = false)]
        [StringLength(50, ErrorMessage = "{0}stringmax{1}")]
        public string NewPassword { get; set; }

        [Display(Name = "NewPasswordComfirm")]
        [Required(AllowEmptyStrings = false)]
        [StringLength(50, ErrorMessage = "{0}stringmax{1}")]
        public string NewPasswordComfirm { get; set; }

        public override void Validate()
        {
            List<ValidationResult> rv = new List<ValidationResult>();
            if (DC.Set<FrameworkUserBase>().Where(x => x.ITCode == LoginUserInfo.ITCode && x.Password == Utils.GetMD5String(OldPassword)).SingleOrDefault() == null)
            {
                MSD.AddModelError("OldPassword", Localizer["OldPasswrodWrong"]);
            }
            if (NewPassword != NewPasswordComfirm)
            {
                MSD.AddModelError("NewPasswordComfirm", Localizer["PasswordNotSame"]);
            }
        }

        public void DoChange()
        {
            var user = DC.Set<FrameworkUserBase>().Where(x => x.ITCode == LoginUserInfo.ITCode).SingleOrDefault();
            if (user != null)
            {
                user.Password = Utils.GetMD5String(NewPassword);
            }
            DC.SaveChanges();
        }
    }
}
