using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using ERCS.Controllers;
using ERCS.ViewModel.Data.LocationVMs;
using ERCS.Model;
using ERCS.DataAccess;

namespace ERCS.Test
{
    [TestClass]
    public class LocationControllerTest
    {
        private LocationController _controller;
        private string _seed;

        public LocationControllerTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateController<LocationController>(_seed, "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            string rv2 = _controller.Search(rv.Model as LocationListVM);
            Assert.IsTrue(rv2.Contains("\"Code\":200"));
        }

        [TestMethod]
        public void CreateTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Create();
            Assert.IsInstanceOfType(rv.Model, typeof(LocationVM));

            LocationVM vm = rv.Model as LocationVM;
            Location v = new Location();
			
            v.Name = "bhVtNC";
            vm.Entity = v;
            _controller.Create(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<Location>().FirstOrDefault();
				
                Assert.AreEqual(data.Name, "bhVtNC");
            }

        }

        [TestMethod]
        public void EditTest()
        {
            Location v = new Location();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.Name = "bhVtNC";
                context.Set<Location>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Edit(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(LocationVM));

            LocationVM vm = rv.Model as LocationVM;
            v = new Location();
            v.ID = vm.Entity.ID;
       		
            v.Name = "GV9Ll";
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.Name", "");
            _controller.Edit(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<Location>().FirstOrDefault();
 				
                Assert.AreEqual(data.Name, "GV9Ll");
            }

        }


        [TestMethod]
        public void DeleteTest()
        {
            Location v = new Location();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.Name = "bhVtNC";
                context.Set<Location>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Delete(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(LocationVM));

            LocationVM vm = rv.Model as LocationVM;
            v = new Location();
            v.ID = vm.Entity.ID;
            vm.Entity = v;
            _controller.Delete(v.ID.ToString(),null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                Assert.AreEqual(context.Set<Location>().Count(), 0);
            }

        }


        [TestMethod]
        public void DetailsTest()
        {
            Location v = new Location();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v.Name = "bhVtNC";
                context.Set<Location>().Add(v);
                context.SaveChanges();
            }
            PartialViewResult rv = (PartialViewResult)_controller.Details(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(IBaseCRUDVM<TopBasePoco>));
            Assert.AreEqual(v.ID, (rv.Model as IBaseCRUDVM<TopBasePoco>).Entity.GetID());
        }

        [TestMethod]
        public void BatchDeleteTest()
        {
            Location v1 = new Location();
            Location v2 = new Location();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.Name = "bhVtNC";
                v2.Name = "GV9Ll";
                context.Set<Location>().Add(v1);
                context.Set<Location>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(LocationBatchVM));

            LocationBatchVM vm = rv.Model as LocationBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            _controller.DoBatchDelete(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                Assert.AreEqual(context.Set<Location>().Count(), 0);
            }
        }


    }
}
