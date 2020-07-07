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
    public partial class VirusSearcher : BaseSearcher
    {
        [Display(Name = "病毒名稱")]
        public String VirusName { get; set; }
        [Display(Name = "病毒號碼")]
        public String VirusCode { get; set; }
        [Display(Name = "病毒種類")]
        public VirusTypeEnum? VirusType { get; set; }

        protected override void InitVM()
        {
        }

    }
}
