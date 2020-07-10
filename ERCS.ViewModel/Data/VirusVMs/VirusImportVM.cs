using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using ERCS.Model;


namespace ERCS.ViewModel.Data.VirusVMs
{
    public partial class VirusTemplateVM : BaseTemplateVM
    {
        [Display(Name = "病毒名稱")]
        public ExcelPropety VirusName_Excel = ExcelPropety.CreateProperty<Virus>(x => x.VirusName);
        [Display(Name = "病毒編號")]
        public ExcelPropety VirusCode_Excel = ExcelPropety.CreateProperty<Virus>(x => x.VirusCode);
        [Display(Name = "病毒描述")]
        public ExcelPropety Remark_Excel = ExcelPropety.CreateProperty<Virus>(x => x.Remark);
        [Display(Name = "病毒種類")]
        public ExcelPropety VirusType_Excel = ExcelPropety.CreateProperty<Virus>(x => x.VirusType);

	    protected override void InitVM()
        {
        }

    }

    public class VirusImportVM : BaseImportVM<VirusTemplateVM, Virus>
    {

    }

}
