using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using ERCS.Controllers;
using ERCS.ViewModel.Data.HospitalVMs;
using ERCS.Model;
using ERCS.DataAccess;

namespace ERCS.Test
{
    [TestClass]
    public class HospitalControllerTest
    {
        private HospitalController _controller;
        private string _seed;

        public HospitalControllerTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateController<HospitalController>(_seed, "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            string rv2 = _controller.Search(rv.Model as HospitalListVM);
            Assert.IsTrue(rv2.Contains("\"Code\":200"));
        }

        [TestMethod]
        public void CreateTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Create();
            Assert.IsInstanceOfType(rv.Model, typeof(HospitalVM));

            HospitalVM vm = rv.Model as HospitalVM;
            Hospital v = new Hospital();
			
            v.Name = "P2yWI";
            vm.Entity = v;
            _controller.Create(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<Hospital>().FirstOrDefault();
				
                Assert.AreEqual(data.Name, "P2yWI");
            }

        }

        [TestMethod]
        public void EditTest()
        {
            Hospital v = new Hospital();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.Name = "P2yWI";
                context.Set<Hospital>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Edit(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(HospitalVM));

            HospitalVM vm = rv.Model as HospitalVM;
            v = new Hospital();
            v.ID = vm.Entity.ID;
       		
            v.Name = "Une5t";
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.Name", "");
            _controller.Edit(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<Hospital>().FirstOrDefault();
 				
                Assert.AreEqual(data.Name, "Une5t");
            }

        }


        [TestMethod]
        public void DeleteTest()
        {
            Hospital v = new Hospital();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.Name = "P2yWI";
                context.Set<Hospital>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Delete(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(HospitalVM));

            HospitalVM vm = rv.Model as HospitalVM;
            v = new Hospital();
            v.ID = vm.Entity.ID;
            vm.Entity = v;
            _controller.Delete(v.ID.ToString(),null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                Assert.AreEqual(context.Set<Hospital>().Count(), 0);
            }

        }


        [TestMethod]
        public void DetailsTest()
        {
            Hospital v = new Hospital();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v.Name = "P2yWI";
                context.Set<Hospital>().Add(v);
                context.SaveChanges();
            }
            PartialViewResult rv = (PartialViewResult)_controller.Details(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(IBaseCRUDVM<TopBasePoco>));
            Assert.AreEqual(v.ID, (rv.Model as IBaseCRUDVM<TopBasePoco>).Entity.GetID());
        }

        [TestMethod]
        public void BatchDeleteTest()
        {
            Hospital v1 = new Hospital();
            Hospital v2 = new Hospital();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.Name = "P2yWI";
                v2.Name = "Une5t";
                context.Set<Hospital>().Add(v1);
                context.Set<Hospital>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(HospitalBatchVM));

            HospitalBatchVM vm = rv.Model as HospitalBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            _controller.DoBatchDelete(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                Assert.AreEqual(context.Set<Hospital>().Count(), 0);
            }
        }


    }
}
