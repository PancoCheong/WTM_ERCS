using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using ERCS.Model;


namespace ERCS.ViewModel.Data.LocationVMs
{
    public partial class LocationListVM : BasePagedListVM<Location_View, LocationSearcher>
    {
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeStandardAction("Location", GridActionStandardTypesEnum.Create, Localizer["Create"],"Data", dialogWidth: 800),
                this.MakeStandardAction("Location", GridActionStandardTypesEnum.Edit, Localizer["Edit"],"Data", dialogWidth: 800),
                this.MakeStandardAction("Location", GridActionStandardTypesEnum.Delete, Localizer["Delete"], "Data",dialogWidth: 800),
                this.MakeStandardAction("Location", GridActionStandardTypesEnum.Details, Localizer["Details"],"Data", dialogWidth: 800),
                this.MakeStandardAction("Location", GridActionStandardTypesEnum.BatchEdit, Localizer["BatchEdit"],"Data", dialogWidth: 800),
                this.MakeStandardAction("Location", GridActionStandardTypesEnum.BatchDelete, Localizer["BatchDelete"],"Data", dialogWidth: 800),
                this.MakeStandardAction("Location", GridActionStandardTypesEnum.Import, Localizer["Import"],"Data", dialogWidth: 800),
                this.MakeStandardAction("Location", GridActionStandardTypesEnum.ExportExcel, Localizer["Export"],"Data"),
            };
        }

        protected override IEnumerable<IGridColumn<Location_View>> InitGridHeader()
        {
            return new List<GridColumn<Location_View>>{
                this.MakeGridHeader(x => x.Name),
                this.MakeGridHeader(x => x.Name_view),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<Location_View> GetSearchQuery()
        {
            var query = DC.Set<Location>()
                .CheckContain(Searcher.Name, x=>x.Name)
                .Select(x => new Location_View
                {
				    ID = x.ID,
                    Name = x.Name,
                    Name_view = x.Parent.Name,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class Location_View : Location{
        [Display(Name = "名稱")]
        public String Name_view { get; set; }

    }
}
