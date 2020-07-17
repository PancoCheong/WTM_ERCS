﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using ERCS.Model;


namespace ERCS.ViewModel.API.PatientVMs
{
    public partial class PatientApiListVM : BasePagedListVM<PatientApi_View, PatientApiSearcher>
    {
        // API will not use this method as it is for UI. CodeGen should omit it.
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeStandardAction("Patient", GridActionStandardTypesEnum.Create, Localizer["Create"],"API", dialogWidth: 800),
                this.MakeStandardAction("Patient", GridActionStandardTypesEnum.Edit, Localizer["Edit"],"API", dialogWidth: 800),
                this.MakeStandardAction("Patient", GridActionStandardTypesEnum.Delete, Localizer["Delete"], "API",dialogWidth: 800),
                this.MakeStandardAction("Patient", GridActionStandardTypesEnum.Details, Localizer["Details"],"API", dialogWidth: 800),
                this.MakeStandardAction("Patient", GridActionStandardTypesEnum.BatchEdit, Localizer["BatchEdit"],"API", dialogWidth: 800),
                this.MakeStandardAction("Patient", GridActionStandardTypesEnum.BatchDelete, Localizer["BatchDelete"],"API", dialogWidth: 800),
                this.MakeStandardAction("Patient", GridActionStandardTypesEnum.Import, Localizer["Import"],"API", dialogWidth: 800),
                this.MakeStandardAction("Patient", GridActionStandardTypesEnum.ExportExcel, Localizer["Export"],"API"),
            };
        }

        protected override IEnumerable<IGridColumn<PatientApi_View>> InitGridHeader()
        {
            return new List<GridColumn<PatientApi_View>>{
                this.MakeGridHeader(x => x.PatientName),
                this.MakeGridHeader(x => x.IdNumber),
                this.MakeGridHeader(x => x.Gender),
                this.MakeGridHeader(x => x.Status),
                this.MakeGridHeader(x => x.Birthday),
                this.MakeGridHeader(x => x.Name_view),
                this.MakeGridHeader(x => x.Name_view2),
                this.MakeGridHeader(x => x.PhotoId).SetFormat(PhotoIdFormat), // WTM will skip .SetFormat(PhotoIdFormat) at runtime as it is for UI formatting
                this.MakeGridHeader(x => x.VirusName_view),
                this.MakeGridHeaderAction(width: 200)
            };
        }
        private List<ColumnFormatInfo> PhotoIdFormat(PatientApi_View entity, object val)
        {
            return new List<ColumnFormatInfo>
            {
                ColumnFormatInfo.MakeDownloadButton(ButtonTypesEnum.Button,entity.PhotoId),
                ColumnFormatInfo.MakeViewButton(ButtonTypesEnum.Button,entity.PhotoId,640,480),
            };
        }


        public override IOrderedQueryable<PatientApi_View> GetSearchQuery()
        {
            var query = DC.Set<Patient>()
                .CheckContain(Searcher.PatientName, x=>x.PatientName)
                .CheckContain(Searcher.IdNumber, x=>x.IdNumber)
                .CheckEqual(Searcher.Gender, x=>x.Gender)
                .CheckEqual(Searcher.Status, x=>x.Status)
                .CheckBetween(Searcher.Birthday?.GetStartTime(), Searcher.Birthday?.GetEndTime(), x => x.Birthday, includeMax: false)
                .CheckEqual(Searcher.LocationId, x=>x.LocationId)
                .CheckEqual(Searcher.HospitalId, x=>x.HospitalId)
                .CheckWhere(Searcher.SelectedVirusesIDs,x=>DC.Set<PatientVirus>().Where(y=>Searcher.SelectedVirusesIDs.Contains(y.VirusId)).Select(z=>z.PatientId).Contains(x.ID))
                .Select(x => new PatientApi_View
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

    public class PatientApi_View : Patient{
        [Display(Name = "名稱")]
        public String Name_view { get; set; }
        [Display(Name = "醫院名稱")]
        public String Name_view2 { get; set; }
        [Display(Name = "病毒名稱")]
        public String VirusName_view { get; set; }

    }
}
