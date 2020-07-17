using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using WalkingTec.Mvvm.Mvc;
using WalkingTec.Mvvm.Core.Auth.Attribute;
using ERCS.ViewModel.API.PatientVMs;
using ERCS.Model;

namespace ERCS.Controllers
{
    [Area("API")]
    [AuthorizeJwt]
    [ActionDescription("病例API")]
    [ApiController] // this controller is for API. If missing, you need to specify [FromBody] on each method parameter
    [Route("api/Patient")] // must specify URL Route as it ignores the default routing rule in HomeController
	public partial class PatientApiController : BaseApiController
    {
        [ActionDescription("Search")]
        [HttpPost("Search")] // must specify request type
		public IActionResult Search(PatientApiSearcher searcher) //Searcher -- search JSON data submit thru API
		//public IActionResult Search([FromBody] PatientApiSearcher searcher)
        {
            if (ModelState.IsValid)
            {
                var vm = CreateVM<PatientApiListVM>();  // VM doesn't have data binding logic, only business logic 
                vm.Searcher = searcher;
                return Content(vm.GetJson());           // return JSON in most case, sometimes File document
            }
            else
            {
                return BadRequest(ModelState.GetErrorJson());
            }
        }

        [ActionDescription("Get")]
        [HttpGet("{id}")]
        public PatientApiVM Get(string id)
        {
            var vm = CreateVM<PatientApiVM>(id);
            return vm;                          //return JSON data
        }

        [ActionDescription("Create")]
        [HttpPost("Add")]
        public IActionResult Add(PatientApiVM vm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorJson());
            }
            else
            {
                vm.DoAdd();
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState.GetErrorJson());
                }
                else
                {
                    return Ok(vm.Entity);       //return status code 200 with JSON data
                }
            }

        }

        [ActionDescription("Edit")]
        [HttpPut("Edit")]
        public IActionResult Edit(PatientApiVM vm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorJson());
            }
            else
            {
                vm.DoEdit(false);
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState.GetErrorJson());
                }
                else
                {
                    return Ok(vm.Entity);
                }
            }
        }

		[HttpPost("BatchDelete")]
        [ActionDescription("Delete")]
        public IActionResult BatchDelete(string[] ids)
        {
            var vm = CreateVM<PatientApiBatchVM>();
            if (ids != null && ids.Count() > 0)
            {
                vm.Ids = ids;
            }
            else
            {
                return Ok();
            }
            if (!ModelState.IsValid || !vm.DoBatchDelete())
            {
                return BadRequest(ModelState.GetErrorJson());
            }
            else
            {
                return Ok(ids.Count());
            }
        }


        [ActionDescription("Export")]
        [HttpPost("ExportExcel")]
        public IActionResult ExportExcel(PatientApiSearcher searcher)
        {
            var vm = CreateVM<PatientApiListVM>();
            vm.Searcher = searcher;
            vm.SearcherMode = ListVMSearchModeEnum.Export;
            return vm.GetExportData();
        }

        [ActionDescription("CheckExport")]
        [HttpPost("ExportExcelByIds")]
        public IActionResult ExportExcelByIds(string[] ids)
        {
            var vm = CreateVM<PatientApiListVM>();
            if (ids != null && ids.Count() > 0)
            {
                vm.Ids = new List<string>(ids);
                vm.SearcherMode = ListVMSearchModeEnum.CheckExport;
            }
            return vm.GetExportData();
        }

        [ActionDescription("DownloadTemplate")]
        [HttpGet("GetExcelTemplate")]
        public IActionResult GetExcelTemplate()
        {
            var vm = CreateVM<PatientApiImportVM>();
            var qs = new Dictionary<string, string>();
            foreach (var item in Request.Query.Keys)
            {
                qs.Add(item, Request.Query[item]);
            }
            vm.SetParms(qs);
            var data = vm.GenerateTemplate(out string fileName);
            return File(data, "application/vnd.ms-excel", fileName);        //return Excel file
        }

        [ActionDescription("Import")]
        [HttpPost("Import")]
        public ActionResult Import(PatientApiImportVM vm)
        {

            if (vm.ErrorListVM.EntityList.Count > 0 || !vm.BatchSaveData())
            {
                return BadRequest(vm.GetErrorJson());
            }
            else
            {
                return Ok(vm.EntityList.Count);
            }
        }

        // related data (unlike UI which can do async query in separate thread), API return both data at once query
        [HttpGet("GetLocations")]   
        public ActionResult GetLocations()
        {
            return Ok(DC.Set<Location>().GetSelectListItems(LoginUserInfo?.DataPrivileges, null, x => x.Name));
        }

        [HttpGet("GetHospitals")]
        public ActionResult GetHospitals()
        {
            return Ok(DC.Set<Hospital>().GetSelectListItems(LoginUserInfo?.DataPrivileges, null, x => x.Name));
        }

        [HttpGet("GetViruss")]
        public ActionResult GetViruss()
        {
            return Ok(DC.Set<Virus>().GetSelectListItems(LoginUserInfo?.DataPrivileges, null, x => x.VirusName));
        }

    }
}
