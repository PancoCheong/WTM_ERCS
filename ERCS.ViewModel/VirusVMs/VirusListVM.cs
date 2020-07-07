using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using ERCS.Model;


namespace ERCS.ViewModel.VirusVMs
{
    public partial class VirusListVM : BasePagedListVM<Virus_View, VirusSearcher>
    {
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeStandardAction("Virus", GridActionStandardTypesEnum.Create, Localizer["Create"],"", dialogWidth: 800),
                this.MakeStandardAction("Virus", GridActionStandardTypesEnum.Edit, Localizer["Edit"],"", dialogWidth: 800),
                this.MakeStandardAction("Virus", GridActionStandardTypesEnum.Delete, Localizer["Delete"], "",dialogWidth: 800),
                this.MakeStandardAction("Virus", GridActionStandardTypesEnum.Details, Localizer["Details"],"", dialogWidth: 800),
                this.MakeStandardAction("Virus", GridActionStandardTypesEnum.BatchEdit, Localizer["BatchEdit"],"", dialogWidth: 800),
                this.MakeStandardAction("Virus", GridActionStandardTypesEnum.BatchDelete, Localizer["BatchDelete"],"", dialogWidth: 800),
                this.MakeStandardAction("Virus", GridActionStandardTypesEnum.Import, Localizer["Import"],"", dialogWidth: 800),
                this.MakeStandardAction("Virus", GridActionStandardTypesEnum.ExportExcel, Localizer["Export"],""),
            };
        }

        protected override IEnumerable<IGridColumn<Virus_View>> InitGridHeader()
        {
            return new List<GridColumn<Virus_View>>{
                this.MakeGridHeader(x => x.VirusName),
                this.MakeGridHeader(x => x.VirusCode),
                this.MakeGridHeader(x => x.VirusType),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<Virus_View> GetSearchQuery()
        {
            var query = DC.Set<Virus>()
                .CheckContain(Searcher.VirusName, x=>x.VirusName)
                .CheckContain(Searcher.VirusCode, x=>x.VirusCode)
                .CheckEqual(Searcher.VirusType, x=>x.VirusType)
                .Select(x => new Virus_View
                {
				    ID = x.ID,
                    VirusName = x.VirusName,
                    VirusCode = x.VirusCode,
                    VirusType = x.VirusType,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class Virus_View : Virus{

    }
}
