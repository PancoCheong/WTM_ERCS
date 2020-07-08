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
    public partial class LocationBatchVM : BaseBatchVM<Location, Location_BatchEdit>
    {
        public LocationBatchVM()
        {
            ListVM = new LocationListVM();
            LinkedVM = new Location_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class Location_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
