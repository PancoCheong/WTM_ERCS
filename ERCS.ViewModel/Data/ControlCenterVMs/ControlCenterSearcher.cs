using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using ERCS.Model;


namespace ERCS.ViewModel.Data.ControlCenterVMs
{
    public partial class ControlCenterSearcher : BaseSearcher
    {
        [Display(Name = "中心名稱")]
        public String Name { get; set; }

        protected override void InitVM()
        {
        }

    }
}
