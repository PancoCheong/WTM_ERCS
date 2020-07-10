using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using ERCS.Model;


namespace ERCS.ViewModel.Tracking.ReportVMs
{
    public partial class ReportListVM : BasePagedListVM<Report_View, ReportSearcher>
    {
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeStandardAction("Report", GridActionStandardTypesEnum.Create, Localizer["Create"],"Tracking", dialogWidth: 800),
                this.MakeStandardAction("Report", GridActionStandardTypesEnum.Edit, Localizer["Edit"],"Tracking", dialogWidth: 800),
                this.MakeStandardAction("Report", GridActionStandardTypesEnum.Delete, Localizer["Delete"], "Tracking",dialogWidth: 800),
                this.MakeStandardAction("Report", GridActionStandardTypesEnum.Details, Localizer["Details"],"Tracking", dialogWidth: 800),
                this.MakeStandardAction("Report", GridActionStandardTypesEnum.BatchEdit, Localizer["BatchEdit"],"Tracking", dialogWidth: 800),
                this.MakeStandardAction("Report", GridActionStandardTypesEnum.BatchDelete, Localizer["BatchDelete"],"Tracking", dialogWidth: 800),
                this.MakeStandardAction("Report", GridActionStandardTypesEnum.Import, Localizer["Import"],"Tracking", dialogWidth: 800),
                this.MakeStandardAction("Report", GridActionStandardTypesEnum.ExportExcel, Localizer["Export"],"Tracking"),
            };
        }

        protected override IEnumerable<IGridColumn<Report_View>> InitGridHeader()
        {
            return new List<GridColumn<Report_View>>{
                this.MakeGridHeader(x => x.Temperature),
                this.MakeGridHeader(x => x.Remark),
                this.MakeGridHeader(x => x.PatientName_view),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<Report_View> GetSearchQuery()
        {
            var query = DC.Set<Report>()
                .CheckEqual(Searcher.Temperature, x=>x.Temperature)
                .CheckEqual(Searcher.PatientId, x=>x.PatientId)
                .Select(x => new Report_View
                {
				    ID = x.ID,
                    Temperature = x.Temperature,
                    Remark = x.Remark,
                    PatientName_view = x.Patient.PatientName,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class Report_View : Report{
        [Display(Name = "患者名稱")]
        public String PatientName_view { get; set; }

    }
}
