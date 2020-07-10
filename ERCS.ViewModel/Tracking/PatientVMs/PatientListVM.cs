using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using ERCS.Model;


namespace ERCS.ViewModel.Tracking.PatientVMs
{
    public partial class PatientListVM : BasePagedListVM<Patient_View, PatientSearcher>
    {
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeStandardAction("Patient", GridActionStandardTypesEnum.Create, Localizer["Create"],"Tracking", dialogWidth: 800),
                this.MakeStandardAction("Patient", GridActionStandardTypesEnum.Edit, Localizer["Edit"],"Tracking", dialogWidth: 800),
                this.MakeStandardAction("Patient", GridActionStandardTypesEnum.Delete, Localizer["Delete"], "Tracking",dialogWidth: 800),
                this.MakeStandardAction("Patient", GridActionStandardTypesEnum.Details, Localizer["Details"],"Tracking", dialogWidth: 800),
                this.MakeStandardAction("Patient", GridActionStandardTypesEnum.BatchEdit, Localizer["BatchEdit"],"Tracking", dialogWidth: 800),
                this.MakeStandardAction("Patient", GridActionStandardTypesEnum.BatchDelete, Localizer["BatchDelete"],"Tracking", dialogWidth: 800),
                this.MakeStandardAction("Patient", GridActionStandardTypesEnum.Import, Localizer["Import"],"Tracking", dialogWidth: 800),
                this.MakeStandardAction("Patient", GridActionStandardTypesEnum.ExportExcel, Localizer["Export"],"Tracking"),
            };
        }

        protected override IEnumerable<IGridColumn<Patient_View>> InitGridHeader()
        {
            return new List<GridColumn<Patient_View>>{
                this.MakeGridHeader(x => x.PatientName),
                this.MakeGridHeader(x => x.IdNumber),
                this.MakeGridHeader(x => x.Gender),
                this.MakeGridHeader(x => x.Status),
                this.MakeGridHeader(x => x.Birthday),
                this.MakeGridHeader(x => x.Name_view),
                this.MakeGridHeader(x => x.Name_view2),
                this.MakeGridHeader(x => x.PhotoId).SetFormat(PhotoIdFormat),
                this.MakeGridHeader(x => x.VirusName_view),
                this.MakeGridHeaderAction(width: 200)
            };
        }
        private List<ColumnFormatInfo> PhotoIdFormat(Patient_View entity, object val)
        {
            return new List<ColumnFormatInfo>
            {
                ColumnFormatInfo.MakeDownloadButton(ButtonTypesEnum.Button,entity.PhotoId),
                ColumnFormatInfo.MakeViewButton(ButtonTypesEnum.Button,entity.PhotoId,640,480),
            };
        }


        public override IOrderedQueryable<Patient_View> GetSearchQuery()
        {
            var query = DC.Set<Patient>()
                .CheckContain(Searcher.PatientName, x=>x.PatientName)
                .CheckContain(Searcher.IdNumber, x=>x.IdNumber)
                .CheckEqual(Searcher.Gender, x=>x.Gender)
                .CheckEqual(Searcher.Status, x=>x.Status)
                .CheckEqual(Searcher.LocationId, x=>x.LocationId)
                .CheckEqual(Searcher.HospitalId, x=>x.HospitalId)
                .CheckWhere(Searcher.SelectedVirusesIDs,x=>DC.Set<PatientVirus>().Where(y=>Searcher.SelectedVirusesIDs.Contains(y.VirusId)).Select(z=>z.PatientId).Contains(x.ID))
                .Select(x => new Patient_View
                {
				    ID = x.ID,
                    PatientName = x.PatientName,
                    IdNumber = x.IdNumber,
                    Gender = x.Gender,
                    Status = x.Status,
                    Birthday = x.Birthday,
                    Name_view = x.Location.Name,
                    Name_view2 = x.Hospital.Name,
                    PhotoId = x.PhotoId,
                    VirusName_view = x.Viruses.Select(y=>y.Virus.VirusName).ToSpratedString(null,","), 
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class Patient_View : Patient{
        [Display(Name = "名稱")]
        public String Name_view { get; set; }
        [Display(Name = "醫院名稱")]
        public String Name_view2 { get; set; }
        [Display(Name = "病毒名稱")]
        public String VirusName_view { get; set; }

    }
}
