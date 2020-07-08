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
    public partial class LocationTemplateVM : BaseTemplateVM
    {
        [Display(Name = "名稱")]
        public ExcelPropety Name_Excel = ExcelPropety.CreateProperty<Location>(x => x.Name);
        [Display(Name = "父級")]
        public ExcelPropety Parent_Excel = ExcelPropety.CreateProperty<Location>(x => x.ParentId);

	    protected override void InitVM()
        {
            Parent_Excel.DataType = ColumnDataType.ComboBox;
            Parent_Excel.ListItems = DC.Set<Location>().GetSelectListItems(LoginUserInfo?.DataPrivileges, null, y => y.Name);
        }

    }

    public class LocationImportVM : BaseImportVM<LocationTemplateVM, Location>
    {

    }

}
