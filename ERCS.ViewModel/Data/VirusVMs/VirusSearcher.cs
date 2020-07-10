using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using ERCS.Model;


namespace ERCS.ViewModel.Data.VirusVMs
{
    public partial class VirusSearcher : BaseSearcher
    {
        [Display(Name = "病毒名稱")]
        public String VirusName { get; set; }
        [Display(Name = "病毒種類")]
        public VirusTypeEnum? VirusType { get; set; }
        public List<ComboSelectListItem> AllPatientss { get; set; }
        [Display(Name = "患者")]
        public List<int> SelectedPatientsIDs { get; set; }

        protected override void InitVM()
        {
            AllPatientss = DC.Set<Patient>().GetSelectListItems(LoginUserInfo?.DataPrivileges, null, y => y.PatientName);
        }

    }
}
