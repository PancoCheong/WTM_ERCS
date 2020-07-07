using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using ERCS.Model;


namespace ERCS.ViewModel.VirusVMs
{
    public partial class VirusBatchVM : BaseBatchVM<Virus, Virus_BatchEdit>
    {
        public VirusBatchVM()
        {
            ListVM = new VirusListVM();
            LinkedVM = new Virus_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class Virus_BatchEdit : BaseVM
    {
        [Display(Name = "病毒種類")]
        public VirusTypeEnum? VirusType { get; set; }

        protected override void InitVM()
        {
        }

    }

}
