using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using ERCS.Model;


namespace ERCS.ViewModel.Tracking.ReportVMs
{
    public partial class ReportVM : BaseCRUDVM<Report>
    {
        public List<ComboSelectListItem> AllPatients { get; set; }

        public ReportVM()
        {
            SetInclude(x => x.Patient);
        }

        protected override void InitVM()
        {
            AllPatients = DC.Set<Patient>().GetSelectListItems(LoginUserInfo?.DataPrivileges, null, y => y.PatientName);
        }

        public override void DoAdd()
        {           
            base.DoAdd();
        }

        public override void DoEdit(bool updateAllFields = false)
        {
            base.DoEdit(updateAllFields);
        }

        public override void DoDelete()
        {
            base.DoDelete();
        }
    }
}
