﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using ERCS.Controllers;
using ERCS.ViewModel.HomeVMs;
using ERCS.DataAccess;
using WalkingTec.Mvvm.Mvc;

namespace ERCS.Test
{
    [TestClass]
    public class LoginControllerTest
    {
        private LoginController _controller;
        private string _seed;

        public LoginControllerTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateController<LoginController>(_seed, "user");
        }

        [TestMethod]
        public void LoginTest()
        {
            ViewResult rv = (ViewResult)_controller.Login();
            Assert.IsInstanceOfType(rv.Model, typeof(LoginVM));

            FrameworkUserBase v = new FrameworkUserBase();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                v.ITCode = "itcode";
                v.Name = "name";
                v.Password = Utils.GetMD5String("password");
                v.IsValid = true;
                context.Set<FrameworkUserBase>().Add(v);
                context.SaveChanges();
            }

            LoginVM vm = rv.Model as LoginVM;
            vm.ITCode = "itcode";
            vm.Password = "password";
            vm.VerifyCode = "abcd";
            _controller.HttpContext.Session.Set("verify_code","abcd");
            var rv2 = _controller.Login(vm);

            Assert.AreEqual(_controller.LoginUserInfo.ITCode, "itcode");
        }

        [TestMethod]
        public void ChangePassword()
        {
            FrameworkUserBase v = new FrameworkUserBase();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                v.ITCode = "user";
                v.Name = "name";
                v.Password = Utils.GetMD5String("password");
                v.IsValid = true;
                context.Set<FrameworkUserBase>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.ChangePassword();
            Assert.IsInstanceOfType(rv.Model, typeof( ChangePasswordVM));

            ChangePasswordVM vm = rv.Model as ChangePasswordVM;
            vm.ITCode = "user";
            vm.OldPassword = "password";
            vm.NewPassword = "p1";
            vm.NewPasswordComfirm = "p1";
            var rv2 = _controller.ChangePassword(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var u = context.Set<FrameworkUserBase>().FirstOrDefault();
                Assert.AreEqual(u.Password, Utils.GetMD5String("p1"));
            }

            Assert.IsInstanceOfType(rv2, typeof(FResult));
        }
    }
}
