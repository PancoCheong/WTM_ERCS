using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using ERCS.Model;


namespace ERCS.ViewModel.Tracking.ReportVMs
{
    public partial class ReportSearcher : BaseSearcher
    {
        [Display(Name = "體溫")]
        public Single? Temperature { get; set; }
        public List<ComboSelectListItem> AllPatients { get; set; }
        [Display(Name = "患者名稱")]
        public int? PatientId { get; set; }

        protected override void InitVM()
        {
            AllPatients = DC.Set<Patient>().GetSelectListItems(LoginUserInfo?.DataPrivileges, null, y => y.PatientName);
        }

    }
}
