using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using ERCS.Model;


namespace ERCS.ViewModel.Data.HospitalVMs
{
    public partial class HospitalListVM : BasePagedListVM<Hospital_View, HospitalSearcher>
    {
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeStandardAction("Hospital", GridActionStandardTypesEnum.Create, Localizer["Create"],"Data", dialogWidth: 800),
                this.MakeStandardAction("Hospital", GridActionStandardTypesEnum.Edit, Localizer["Edit"],"Data", dialogWidth: 800),
                this.MakeStandardAction("Hospital", GridActionStandardTypesEnum.Delete, Localizer["Delete"], "Data",dialogWidth: 800),
                this.MakeStandardAction("Hospital", GridActionStandardTypesEnum.Details, Localizer["Details"],"Data", dialogWidth: 800),
                this.MakeStandardAction("Hospital", GridActionStandardTypesEnum.BatchEdit, Localizer["BatchEdit"],"Data", dialogWidth: 800),
                this.MakeStandardAction("Hospital", GridActionStandardTypesEnum.BatchDelete, Localizer["BatchDelete"],"Data", dialogWidth: 800),
                this.MakeStandardAction("Hospital", GridActionStandardTypesEnum.Import, Localizer["Import"],"Data", dialogWidth: 800),
                this.MakeStandardAction("Hospital", GridActionStandardTypesEnum.ExportExcel, Localizer["Export"],"Data"),
            };
        }

        protected override IEnumerable<IGridColumn<Hospital_View>> InitGridHeader()
        {
            return new List<GridColumn<Hospital_View>>{
                this.MakeGridHeader(x => x.Name),
                this.MakeGridHeader(x => x.Level),
                this.MakeGridHeader(x => x.Name_view),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<Hospital_View> GetSearchQuery()
        {
            var query = DC.Set<Hospital>()
                .CheckContain(Searcher.Name, x=>x.Name)
                .CheckEqual(Searcher.Level, x=>x.Level)
                .Select(x => new Hospital_View
                {
				    ID = x.ID,
                    Name = x.Name,
                    Level = x.Level,
                    Name_view = x.Location.Name,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class Hospital_View : Hospital{
        [Display(Name = "名稱")]
        public String Name_view { get; set; }

    }
}
