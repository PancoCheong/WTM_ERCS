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
    public partial class HospitalTemplateVM : BaseTemplateVM
    {
        [Display(Name = "醫院名稱")]
        public ExcelPropety Name_Excel = ExcelPropety.CreateProperty<Hospital>(x => x.Name);
        [Display(Name = "醫院級別")]
        public ExcelPropety Level_Excel = ExcelPropety.CreateProperty<Hospital>(x => x.Level);
        [Display(Name = "醫院地點")]
        public ExcelPropety Location_Excel = ExcelPropety.CreateProperty<Hospital>(x => x.LocationId);

	    protected override void InitVM()
        {
            Location_Excel.DataType = ColumnDataType.ComboBox;
            Location_Excel.ListItems = DC.Set<Location>().GetSelectListItems(LoginUserInfo?.DataPrivileges, null, y => y.Name);
        }

    }

    public class HospitalImportVM : BaseImportVM<HospitalTemplateVM, Hospital>
    {

    }

}
