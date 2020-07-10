using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using ERCS.Model;


namespace ERCS.ViewModel.Tracking.PatientVMs
{
    public partial class PatientSearcher : BaseSearcher
    {
        [Display(Name = "患者名稱")]
        public String PatientName { get; set; }
        [Display(Name = "證件號碼")]
        public String IdNumber { get; set; }
        [Display(Name = "性別")]
        public GenderEnum? Gender { get; set; }
        [Display(Name = "患者狀態")]
        public PatientStatusEnum? Status { get; set; }
        public List<ComboSelectListItem> AllLocations { get; set; }
        [Display(Name = "藉貫")]
        public Guid? LocationId { get; set; }
        public List<ComboSelectListItem> AllHospitals { get; set; }
        [Display(Name = "所屬醫院")]
        public Guid? HospitalId { get; set; }
        public List<ComboSelectListItem> AllVirusess { get; set; }
        [Display(Name = "病毒")]
        public List<Guid> SelectedVirusesIDs { get; set; }

        protected override void InitVM()
        {
            AllLocations = DC.Set<Location>().GetSelectListItems(LoginUserInfo?.DataPrivileges, null, y => y.Name);
            AllHospitals = DC.Set<Hospital>().GetSelectListItems(LoginUserInfo?.DataPrivileges, null, y => y.Name);
            AllVirusess = DC.Set<Virus>().GetSelectListItems(LoginUserInfo?.DataPrivileges, null, y => y.VirusName);
        }

    }
}
