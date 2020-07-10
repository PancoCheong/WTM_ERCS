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
    public partial class PatientBatchVM : BaseBatchVM<Patient, Patient_BatchEdit>
    {
        public PatientBatchVM()
        {
            ListVM = new PatientListVM();
            LinkedVM = new Patient_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class Patient_BatchEdit : BaseVM
    {
        [Display(Name = "患者狀態")]
        public PatientStatusEnum? Status { get; set; }

        protected override void InitVM()
        {
        }

    }

}
