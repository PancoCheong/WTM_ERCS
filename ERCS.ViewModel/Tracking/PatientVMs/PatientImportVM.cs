using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using ERCS.Model;


namespace ERCS.ViewModel.Tracking.PatientVMs
{
    public partial class PatientTemplateVM : BaseTemplateVM
    {
        [Display(Name = "患者名稱")]
        public ExcelPropety PatientName_Excel = ExcelPropety.CreateProperty<Patient>(x => x.PatientName);
        [Display(Name = "證件號碼")]
        public ExcelPropety IdNumber_Excel = ExcelPropety.CreateProperty<Patient>(x => x.IdNumber);
        [Display(Name = "性別")]
        public ExcelPropety Gender_Excel = ExcelPropety.CreateProperty<Patient>(x => x.Gender);
        [Display(Name = "患者狀態")]
        public ExcelPropety Status_Excel = ExcelPropety.CreateProperty<Patient>(x => x.Status);
        [Display(Name = "出生日期")]
        public ExcelPropety Birthday_Excel = ExcelPropety.CreateProperty<Patient>(x => x.Birthday);
        [Display(Name = "藉貫")]
        public ExcelPropety Location_Excel = ExcelPropety.CreateProperty<Patient>(x => x.LocationId);
        [Display(Name = "所屬醫院")]
        public ExcelPropety Hospital_Excel = ExcelPropety.CreateProperty<Patient>(x => x.HospitalId);

	    protected override void InitVM()
        {
            Location_Excel.DataType = ColumnDataType.ComboBox;
            Location_Excel.ListItems = DC.Set<Location>().GetSelectListItems(LoginUserInfo?.DataPrivileges, null, y => y.Name);
            Hospital_Excel.DataType = ColumnDataType.ComboBox;
            Hospital_Excel.ListItems = DC.Set<Hospital>().GetSelectListItems(LoginUserInfo?.DataPrivileges, null, y => y.Name);
        }

    }

    public class PatientImportVM : BaseImportVM<PatientTemplateVM, Patient>
    {

    }

}
