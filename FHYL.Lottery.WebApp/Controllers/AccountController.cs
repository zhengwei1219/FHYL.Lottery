using FHYL.Lottery.BLL;
using FHYL.Lottery.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FHYL.Lottery.WebApp.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/
        UserInfoService userInfoService = new UserInfoService();
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 较验用户余额
        /// </summary>
        /// <returns></returns>
        public ActionResult CheckBalance()
        {
            string userName = Request["userName"];
            decimal  CashMoney = decimal.Parse(Request["cashMoney"]);
           UserInfo user = userInfoService.LoadEntities(s=>s.Name==userName).FirstOrDefault();
           if (user != null)
           {
               decimal balance = (from a in user.Account select a.Balance).FirstOrDefault();
               if (balance < CashMoney)
               {
                   return Content("余额不足");
               }

           }
           return Content("ok");
        }

    }
}
