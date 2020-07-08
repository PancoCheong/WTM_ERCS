using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using ERCS.Controllers;
using ERCS.ViewModel.Data.ControlCenterVMs;
using ERCS.Model;
using ERCS.DataAccess;

namespace ERCS.Test
{
    [TestClass]
    public class ControlCenterControllerTest
    {
        private ControlCenterController _controller;
        private string _seed;

        public ControlCenterControllerTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateController<ControlCenterController>(_seed, "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            string rv2 = _controller.Search(rv.Model as ControlCenterListVM);
            Assert.IsTrue(rv2.Contains("\"Code\":200"));
        }

        [TestMethod]
        public void CreateTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Create();
            Assert.IsInstanceOfType(rv.Model, typeof(ControlCenterVM));

            ControlCenterVM vm = rv.Model as ControlCenterVM;
            ControlCenter v = new ControlCenter();
			
            v.Name = "OacyBP";
            vm.Entity = v;
            _controller.Create(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<ControlCenter>().FirstOrDefault();
				
                Assert.AreEqual(data.Name, "OacyBP");
            }

        }

        [TestMethod]
        public void EditTest()
        {
            ControlCenter v = new ControlCenter();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.Name = "OacyBP";
                context.Set<ControlCenter>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Edit(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(ControlCenterVM));

            ControlCenterVM vm = rv.Model as ControlCenterVM;
            v = new ControlCenter();
            v.ID = vm.Entity.ID;
       		
            v.Name = "JaeFy";
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.Name", "");
            _controller.Edit(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<ControlCenter>().FirstOrDefault();
 				
                Assert.AreEqual(data.Name, "JaeFy");
            }

        }


        [TestMethod]
        public void DeleteTest()
        {
            ControlCenter v = new ControlCenter();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.Name = "OacyBP";
                context.Set<ControlCenter>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Delete(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(ControlCenterVM));

            ControlCenterVM vm = rv.Model as ControlCenterVM;
            v = new ControlCenter();
            v.ID = vm.Entity.ID;
            vm.Entity = v;
            _controller.Delete(v.ID.ToString(),null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                Assert.AreEqual(context.Set<ControlCenter>().Count(), 0);
            }

        }


        [TestMethod]
        public void DetailsTest()
        {
            ControlCenter v = new ControlCenter();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v.Name = "OacyBP";
                context.Set<ControlCenter>().Add(v);
                context.SaveChanges();
            }
            PartialViewResult rv = (PartialViewResult)_controller.Details(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(IBaseCRUDVM<TopBasePoco>));
            Assert.AreEqual(v.ID, (rv.Model as IBaseCRUDVM<TopBasePoco>).Entity.GetID());
        }

        [TestMethod]
        public void BatchDeleteTest()
        {
            ControlCenter v1 = new ControlCenter();
            ControlCenter v2 = new ControlCenter();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.Name = "OacyBP";
                v2.Name = "JaeFy";
                context.Set<ControlCenter>().Add(v1);
                context.Set<ControlCenter>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(ControlCenterBatchVM));

            ControlCenterBatchVM vm = rv.Model as ControlCenterBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            _controller.DoBatchDelete(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                Assert.AreEqual(context.Set<ControlCenter>().Count(), 0);
            }
        }


    }
}
