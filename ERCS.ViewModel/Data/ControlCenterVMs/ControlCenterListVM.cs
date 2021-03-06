﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using ERCS.Model;


namespace ERCS.ViewModel.Data.ControlCenterVMs
{
    public partial class ControlCenterListVM : BasePagedListVM<ControlCenter_View, ControlCenterSearcher>
    {
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeStandardAction("ControlCenter", GridActionStandardTypesEnum.Create, Localizer["Create"],"Data", dialogWidth: 800),
                this.MakeStandardAction("ControlCenter", GridActionStandardTypesEnum.Edit, Localizer["Edit"],"Data", dialogWidth: 800),
                this.MakeStandardAction("ControlCenter", GridActionStandardTypesEnum.Delete, Localizer["Delete"], "Data",dialogWidth: 800),
                this.MakeStandardAction("ControlCenter", GridActionStandardTypesEnum.Details, Localizer["Details"],"Data", dialogWidth: 800),
                this.MakeStandardAction("ControlCenter", GridActionStandardTypesEnum.BatchEdit, Localizer["BatchEdit"],"Data", dialogWidth: 800),
                this.MakeStandardAction("ControlCenter", GridActionStandardTypesEnum.BatchDelete, Localizer["BatchDelete"],"Data", dialogWidth: 800),
                this.MakeStandardAction("ControlCenter", GridActionStandardTypesEnum.Import, Localizer["Import"],"Data", dialogWidth: 800),
                this.MakeStandardAction("ControlCenter", GridActionStandardTypesEnum.ExportExcel, Localizer["Export"],"Data"),
            };
        }

        protected override IEnumerable<IGridColumn<ControlCenter_View>> InitGridHeader()
        {
            return new List<GridColumn<ControlCenter_View>>{
                this.MakeGridHeader(x => x.Name),
                this.MakeGridHeader(x => x.Name_view),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<ControlCenter_View> GetSearchQuery()
        {
            var query = DC.Set<ControlCenter>()
                .CheckContain(Searcher.Name, x=>x.Name)
                .Select(x => new ControlCenter_View
                {
				    ID = x.ID,
                    Name = x.Name,
                    Name_view = x.Location.Name,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class ControlCenter_View : ControlCenter{
        [Display(Name = "名稱")]
        public String Name_view { get; set; }

    }
}
