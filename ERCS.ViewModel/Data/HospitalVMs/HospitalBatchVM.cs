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
    public partial class HospitalBatchVM : BaseBatchVM<Hospital, Hospital_BatchEdit>
    {
        public HospitalBatchVM()
        {
            ListVM = new HospitalListVM();
            LinkedVM = new Hospital_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class Hospital_BatchEdit : BaseVM
    {
        [Display(Name = "醫院級別")]
        public HospitalLevel? Level { get; set; }
        public List<ComboSelectListItem> AllLocations { get; set; }
        [Display(Name = "醫院地點")]
        public Guid? LocationId { get; set; }

        protected override void InitVM()
        {
            AllLocations = DC.Set<Location>().GetSelectListItems(LoginUserInfo?.DataPrivileges, null, y => y.Name);
        }

    }

}
