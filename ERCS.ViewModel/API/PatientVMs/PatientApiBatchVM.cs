using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using ERCS.Model;


namespace ERCS.ViewModel.API.PatientVMs
{
    public partial class PatientApiBatchVM : BaseBatchVM<Patient, PatientApi_BatchEdit>
    {
        public PatientApiBatchVM()
        {
            ListVM = new PatientApiListVM();
            LinkedVM = new PatientApi_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class PatientApi_BatchEdit : BaseVM
    {
        [Display(Name = "患者狀態")]
        public PatientStatusEnum? Status { get; set; }
        public List<ComboSelectListItem> AllVirusess { get; set; }
        [Display(Name = "病毒")]
        public List<Guid> SelectedVirusesIDs { get; set; }

        protected override void InitVM()
        {
            AllVirusess = DC.Set<Virus>().GetSelectListItems(LoginUserInfo?.DataPrivileges, null, y => y.VirusName);
        }

    }

}
