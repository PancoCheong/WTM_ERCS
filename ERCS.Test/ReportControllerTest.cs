using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using ERCS.Controllers;
using ERCS.ViewModel.Tracking.ReportVMs;
using ERCS.Model;
using ERCS.DataAccess;

namespace ERCS.Test
{
    [TestClass]
    public class ReportControllerTest
    {
        private ReportController _controller;
        private string _seed;

        public ReportControllerTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateController<ReportController>(_seed, "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            string rv2 = _controller.Search(rv.Model as ReportListVM);
            Assert.IsTrue(rv2.Contains("\"Code\":200"));
        }

        [TestMethod]
        public void CreateTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Create();
            Assert.IsInstanceOfType(rv.Model, typeof(ReportVM));

            ReportVM vm = rv.Model as ReportVM;
            Report v = new Report();
			
            v.Temperature = 36;
            vm.Entity = v;
            _controller.Create(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<Report>().FirstOrDefault();
				
                Assert.AreEqual(data.Temperature, 36);
            }

        }

        [TestMethod]
        public void EditTest()
        {
            Report v = new Report();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.Temperature = 36;
                context.Set<Report>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Edit(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(ReportVM));

            ReportVM vm = rv.Model as ReportVM;
            v = new Report();
            v.ID = vm.Entity.ID;
       		
            v.Temperature = 44;
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.Temperature", "");
            _controller.Edit(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<Report>().FirstOrDefault();
 				
                Assert.AreEqual(data.Temperature, 44);
            }

        }


        [TestMethod]
        public void DeleteTest()
        {
            Report v = new Report();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.Temperature = 36;
                context.Set<Report>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Delete(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(ReportVM));

            ReportVM vm = rv.Model as ReportVM;
            v = new Report();
            v.ID = vm.Entity.ID;
            vm.Entity = v;
            _controller.Delete(v.ID.ToString(),null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                Assert.AreEqual(context.Set<Report>().Count(), 0);
            }

        }


        [TestMethod]
        public void DetailsTest()
        {
            Report v = new Report();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v.Temperature = 36;
                context.Set<Report>().Add(v);
                context.SaveChanges();
            }
            PartialViewResult rv = (PartialViewResult)_controller.Details(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(IBaseCRUDVM<TopBasePoco>));
            Assert.AreEqual(v.ID, (rv.Model as IBaseCRUDVM<TopBasePoco>).Entity.GetID());
        }

        [TestMethod]
        public void BatchDeleteTest()
        {
            Report v1 = new Report();
            Report v2 = new Report();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.Temperature = 36;
                v2.Temperature = 44;
                context.Set<Report>().Add(v1);
                context.Set<Report>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(ReportBatchVM));

            ReportBatchVM vm = rv.Model as ReportBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            _controller.DoBatchDelete(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                Assert.AreEqual(context.Set<Report>().Count(), 0);
            }
        }


    }
}
