using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using ERCS.Model;


namespace ERCS.ViewModel.Tracking.ReportVMs
{
    public partial class ReportTemplateVM : BaseTemplateVM
    {
        [Display(Name = "體溫")]
        public ExcelPropety Temperature_Excel = ExcelPropety.CreateProperty<Report>(x => x.Temperature);
        [Display(Name = "備註")]
        public ExcelPropety Remark_Excel = ExcelPropety.CreateProperty<Report>(x => x.Remark);
        [Display(Name = "患者名稱")]
        public ExcelPropety Patient_Excel = ExcelPropety.CreateProperty<Report>(x => x.PatientId);

	    protected override void InitVM()
        {
            Patient_Excel.DataType = ColumnDataType.ComboBox;
            Patient_Excel.ListItems = DC.Set<Patient>().GetSelectListItems(LoginUserInfo?.DataPrivileges, null, y => y.PatientName);
        }

    }

    public class ReportImportVM : BaseImportVM<ReportTemplateVM, Report>
    {

    }

}
