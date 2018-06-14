using FHYL.Lottery.BLL;
using FHYL.Lottery.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FHYL.Lottery.WebApp.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        AccountService accountService = new AccountService();
        UserInfoService userInfoService = new UserInfoService();
        public ActionResult Index()
        {
            if (Session["userInfo"] != null)
            { 
            
            }
            return View();
        }
        //我的
        public ActionResult MyInfo()
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    string userName = User.Identity.Name;
                    UserInfo userInfo = userInfoService.LoadEntities(s => s.Name == userName).FirstOrDefault();
                    Account account = accountService.LoadEntities(s => s.UserId == userInfo.id).FirstOrDefault();
                    ViewBag.Balance = account.Balance;
                }
                else
                {
                    ViewBag.Balance = 0;
                }
                return View();
            }
            catch (Exception ex)
            {

                throw new Exception("获取用户余额出错，原因："+ex.Message);
            }
        }
        //趋势
        public ActionResult Trend()
        {
            return View();
        }
        //充值页面
        public ActionResult RechargePage()
        {
            return View();
        }
        //充值
        public ActionResult Recharge()
        {
            string userName = Request["username"];
            UserInfoService userInfoService = new UserInfoService();
            UserInfo user = userInfoService.LoadEntities(s=>s.Name == userName).FirstOrDefault();
            if (user == null)
            {
                throw new Exception("充值失败，充值用户不存！");
            }
            string money = Request["Money"];
            if (!string.IsNullOrEmpty(money))
            {
                decimal rechargeMoney = decimal.Parse(money);
                if (rechargeMoney > 0)
                {
                    //增加一条充值记录
                    RechargeRecord rechargeRecord = new RechargeRecord();
                    rechargeRecord.userId = user.id;
                    rechargeRecord.RechargeBy = User.Identity.Name;
                    rechargeRecord.RechargeDate = DateTime.Now;
                    rechargeRecord.RechargeMoney = rechargeMoney;
                    RechargeRecordService rrs = new RechargeRecordService();
                    rrs.AddEntity(rechargeRecord);
                    //更新用户账户信息
                    Account account = accountService.LoadEntities(s => s.UserId == user.id).FirstOrDefault();
                    if (account == null)
                    {
                        Account accountNew = new Account();
                        accountNew.UserId = user.id;
                        accountNew.IsValid = true;
                        accountNew.Balance = rechargeMoney;
                        accountService.AddEntity(accountNew);
                    }
                    else
                    {
                        account.Balance = account.Balance + rechargeMoney;
                        accountService.UpdateEntity(account);
                    }
                    
                    return Content("ok");
                }
                else
                {
                    return Content("充值失败，充值金额有误！");
                }
            }
            else
            {
                return Content("充值失败，充值金额为空！");
            }
            
        }
        //提现页面
        public ActionResult CashPage()
        {
            return View();
        }
        //充值
        public ActionResult Cash()
        {
            string userName = Request["username"];
            UserInfoService userInfoService = new UserInfoService();
            UserInfo user = userInfoService.LoadEntities(s => s.Name == userName).FirstOrDefault();
            if (user == null)
            {
                throw new Exception("充值失败，充值用户不存！");
            }
            string money = Request["Money"];
            if (!string.IsNullOrEmpty(money))
            {
                decimal cashMoney = decimal.Parse(money);
                if (cashMoney > 0)
                {
                    
                    //更新用户账户信息
                    Account account = accountService.LoadEntities(s => s.UserId == user.id).FirstOrDefault();
                    if (account.Balance < cashMoney)
                    {
                        return Content("余额不足，提现失败");
                    }
                    if (account != null)
                    {
                        account.Balance = account.Balance - cashMoney;
                        accountService.UpdateEntity(account);
                    }
                    else
                    {
                        return Content("提现失败，当前用户账户信息有误！");
                    }
                    //增加一条充值记录
                    CashRecord cashRecord = new CashRecord();
                    cashRecord.UserId = user.id;
                    cashRecord.CashBy = userName;
                    cashRecord.CashDate = DateTime.Now;
                    cashRecord.CashMoney = cashMoney;
                    CashRecordService crs = new CashRecordService();
                    crs.AddEntity(cashRecord);

                    return Content("ok");
                }
                else
                {
                    return Content("充值失败，充值金额有误！");
                }
            }
            else
            {
                return Content("充值失败，充值金额为空！");
            }

        }
        public ActionResult Test()
        {
            return View();
        }
        //开奖
        public ActionResult RunLottery()
        {
            return View();
        }


        public JsonResult GetLotteryRecordForNewList()
        { 
            List<LotteryResult> retList = new List<LotteryResult>();
            string[] lotteryTypearray = new string[]{"广东快乐十分","广东11选5","重庆时时彩","六合彩","百家乐三公"};
            //取每种彩票最新一期的开奖数据
            for (int i = 0; i < lotteryTypearray.Length; i++)
            {
                string itemType = lotteryTypearray[i];
                LotteryResult item = new LotteryResultService().LoadEntities(s => s.LotteryType.Equals(itemType)).OrderByDescending(s => s.Expect).FirstOrDefault();
                if (item != null) retList.Add(item);
            }
            return Json(retList.OrderByDescending(s=>s.Opentime), JsonRequestBehavior.AllowGet);
        }
      
    }
}
