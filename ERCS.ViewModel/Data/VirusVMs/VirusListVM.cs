using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using ERCS.Model;


namespace ERCS.ViewModel.Data.VirusVMs
{
    public partial class VirusListVM : BasePagedListVM<Virus_View, VirusSearcher>
    {
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeStandardAction("Virus", GridActionStandardTypesEnum.Create, Localizer["Create"],"Data", dialogWidth: 800),
                this.MakeStandardAction("Virus", GridActionStandardTypesEnum.Edit, Localizer["Edit"],"Data", dialogWidth: 800),
                this.MakeStandardAction("Virus", GridActionStandardTypesEnum.Delete, Localizer["Delete"], "Data",dialogWidth: 800),
                this.MakeStandardAction("Virus", GridActionStandardTypesEnum.Details, Localizer["Details"],"Data", dialogWidth: 800),
                this.MakeStandardAction("Virus", GridActionStandardTypesEnum.BatchEdit, Localizer["BatchEdit"],"Data", dialogWidth: 800),
                this.MakeStandardAction("Virus", GridActionStandardTypesEnum.BatchDelete, Localizer["BatchDelete"],"Data", dialogWidth: 800),
                this.MakeStandardAction("Virus", GridActionStandardTypesEnum.Import, Localizer["Import"],"Data", dialogWidth: 800),
                this.MakeStandardAction("Virus", GridActionStandardTypesEnum.ExportExcel, Localizer["Export"],"Data"),
            };
        }

        protected override IEnumerable<IGridColumn<Virus_View>> InitGridHeader()
        {
            return new List<GridColumn<Virus_View>>{
                this.MakeGridHeader(x => x.VirusName),
                this.MakeGridHeader(x => x.VirusCode),
                this.MakeGridHeader(x => x.Remark),
                this.MakeGridHeader(x => x.VirusType),
                this.MakeGridHeader(x => x.PatientName_view),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<Virus_View> GetSearchQuery()
        {
            var query = DC.Set<Virus>()
                .CheckContain(Searcher.VirusName, x=>x.VirusName)
                .CheckEqual(Searcher.VirusType, x=>x.VirusType)
                .CheckWhere(Searcher.SelectedPatientsIDs,x=>DC.Set<PatientVirus>().Where(y=>Searcher.SelectedPatientsIDs.Contains(y.PatientId)).Select(z=>z.VirusId).Contains(x.ID))
                .Select(x => new Virus_View
                {
				    ID = x.ID,
                    VirusName = x.VirusName,
                    VirusCode = x.VirusCode,
                    Remark = x.Remark,
                    VirusType = x.VirusType,
                    PatientName_view = x.Patients.Select(y=>y.Patient.PatientName).ToSpratedString(null,","), 
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class Virus_View : Virus{
        [Display(Name = "患者名稱")]
        public String PatientName_view { get; set; }

    }
}
