using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using ERCS.Model;


namespace ERCS.ViewModel.Data.HospitalVMs
{
    public partial class HospitalSearcher : BaseSearcher
    {
        [Display(Name = "醫院名稱")]
        public String Name { get; set; }
        [Display(Name = "醫院級別")]
        public HospitalLevel? Level { get; set; }

        protected override void InitVM()
        {
        }

    }
}
