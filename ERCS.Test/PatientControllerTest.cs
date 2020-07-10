using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using ERCS.Controllers;
using ERCS.ViewModel.Tracking.PatientVMs;
using ERCS.Model;
using ERCS.DataAccess;

namespace ERCS.Test
{
    [TestClass]
    public class PatientControllerTest
    {
        private PatientController _controller;
        private string _seed;

        public PatientControllerTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateController<PatientController>(_seed, "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            string rv2 = _controller.Search(rv.Model as PatientListVM);
            Assert.IsTrue(rv2.Contains("\"Code\":200"));
        }

        [TestMethod]
        public void CreateTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Create();
            Assert.IsInstanceOfType(rv.Model, typeof(PatientVM));

            PatientVM vm = rv.Model as PatientVM;
            Patient v = new Patient();
			
            v.ID = 8;
            v.PatientName = "FU1m";
            v.IdNumber = "USAs9";
            vm.Entity = v;
            _controller.Create(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<Patient>().FirstOrDefault();
				
                Assert.AreEqual(data.ID, 8);
                Assert.AreEqual(data.PatientName, "FU1m");
                Assert.AreEqual(data.IdNumber, "USAs9");
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }

        }

        [TestMethod]
        public void EditTest()
        {
            Patient v = new Patient();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.ID = 8;
                v.PatientName = "FU1m";
                v.IdNumber = "USAs9";
                context.Set<Patient>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Edit(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(PatientVM));

            PatientVM vm = rv.Model as PatientVM;
            v = new Patient();
            v.ID = vm.Entity.ID;
       		
            v.PatientName = "yGsMGiDZ";
            v.IdNumber = "xQjIG";
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.ID", "");
            vm.FC.Add("Entity.PatientName", "");
            vm.FC.Add("Entity.IdNumber", "");
            _controller.Edit(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<Patient>().FirstOrDefault();
 				
                Assert.AreEqual(data.PatientName, "yGsMGiDZ");
                Assert.AreEqual(data.IdNumber, "xQjIG");
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }


        [TestMethod]
        public void DeleteTest()
        {
            Patient v = new Patient();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.ID = 8;
                v.PatientName = "FU1m";
                v.IdNumber = "USAs9";
                context.Set<Patient>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Delete(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(PatientVM));

            PatientVM vm = rv.Model as PatientVM;
            v = new Patient();
            v.ID = vm.Entity.ID;
            vm.Entity = v;
            _controller.Delete(v.ID.ToString(),null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                Assert.AreEqual(context.Set<Patient>().Count(), 1);
            }

        }


        [TestMethod]
        public void DetailsTest()
        {
            Patient v = new Patient();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v.ID = 8;
                v.PatientName = "FU1m";
                v.IdNumber = "USAs9";
                context.Set<Patient>().Add(v);
                context.SaveChanges();
            }
            PartialViewResult rv = (PartialViewResult)_controller.Details(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(IBaseCRUDVM<TopBasePoco>));
            Assert.AreEqual(v.ID, (rv.Model as IBaseCRUDVM<TopBasePoco>).Entity.GetID());
        }

        [TestMethod]
        public void BatchDeleteTest()
        {
            Patient v1 = new Patient();
            Patient v2 = new Patient();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.ID = 8;
                v1.PatientName = "FU1m";
                v1.IdNumber = "USAs9";
                v2.PatientName = "yGsMGiDZ";
                v2.IdNumber = "xQjIG";
                context.Set<Patient>().Add(v1);
                context.Set<Patient>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(PatientBatchVM));

            PatientBatchVM vm = rv.Model as PatientBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            _controller.DoBatchDelete(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                Assert.AreEqual(context.Set<Patient>().Count(), 2);
            }
        }

        [TestMethod]
        public void ExportTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            IActionResult rv2 = _controller.ExportExcel(rv.Model as PatientListVM);
            Assert.IsTrue((rv2 as FileContentResult).FileContents.Length > 0);
        }


    }
}
