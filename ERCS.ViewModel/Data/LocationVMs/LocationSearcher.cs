using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using ERCS.Model;


namespace ERCS.ViewModel.Data.LocationVMs
{
    public partial class LocationSearcher : BaseSearcher
    {
        [Display(Name = "名稱")]
        public String Name { get; set; }

        protected override void InitVM()
        {
        }

    }
}
