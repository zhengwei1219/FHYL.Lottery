using FHYL.Lottery.BLL;
using FHYL.Lottery.Common;
using FHYL.Lottery.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace FHYL.Lottery.WebApp.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/
        UserInfoService userInfoService = new UserInfoService();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UserLogin()
        {
            string userName = Request["LoginUserName"];
            string password = MD5Encrypt.EncryptString(Request["LoginPwd"]);
            UserInfo user = userInfoService.LoadEntities(s=>s.Name==userName && s.Password==password).FirstOrDefault();
            if (user == null)
            {
                
                return Content("用户名或密码错误！");
            }
            else
            {
                FormsAuthentication.SetAuthCookie(userName,false);
                Session["userInfo"] = user;
                return Content("ok");
            }


        }
        //用户注册页面
        public ActionResult UserRegister()
        {
            return View();
        }

        public ActionResult AddRegisterUser()
        {
            string userName = Request["LoginUserName"];
            string password = Request["LoginPwd"];
            UserInfo exist = userInfoService.LoadEntities(s=>s.Name == userName).FirstOrDefault();
            if (exist != null)
            {
                return Content("注册失败，用户名已被占用");
            }
            UserInfo userInfo = new UserInfo();
            userInfo.Name = userName;
            userInfo.Password = MD5Encrypt.EncryptString(password);
            UserInfo user = userInfoService.AddEntity(userInfo);
            if (user  != null)
            {
                InitUserAccount(user);
               return Content("ok");
            }
            else
            {
               return Content("注册失败");
            }
            
        }
        //初始化用户账户
        private void InitUserAccount(UserInfo user)
        {
            try
            {
                Account account = new Account();
                account.UserId = user.id;
                account.Balance = 0;
                account.IsValid = true;
                AccountService accountService = new AccountService();
                accountService.AddEntity(account);
            }
            catch (Exception ex)
            {
                throw new Exception("用户账户信息初始化失败，原因："+ex.Message);
            }
        }
        public ActionResult ValidateUserName()
        {
            string userName = Request["userName"];
            UserInfo user = userInfoService.LoadEntities(s => s.Name == userName).FirstOrDefault();
            if (user != null)
            {
                return Content("该用户名已被占用");
            }
            else
            {
                return Content("ok");
            }
        }
        public ActionResult LoginOut()
        {
            FormsAuthentication.SignOut();
           return  RedirectToAction("MyInfo", "Home");
        }

      

    }
}
