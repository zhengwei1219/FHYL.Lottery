using FHYL.Lottery.BLL;
using FHYL.Lottery.Common;
using FHYL.Lottery.LotteryLibrary;
using FHYL.Lottery.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace FHYL.Lottery.WebApp.Controllers
{
    public class BuyController : Controller
    {
        LotteryResultService lrService = new LotteryResultService();
        BetRecordService brService = new BetRecordService();
        BetChildRecordService brChildService = new BetChildRecordService();
        UserInfoService userService = new UserInfoService();
        //
        // GET: /Login/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult buy()
        {
            return View();
        }
      
        //广东快乐十分钟
        public ActionResult Klsf()
        {
            //LotteryResult lotteryResult = GetLastLotteryResultByType("广东快乐十分");
            //if (lotteryResult == null) return View();
            //ViewBag.lotteryType = lotteryResult.LotteryType;
            //ViewBag.expect = lotteryResult.Expect;           
            //ViewBag.openttime = lotteryResult.Opentime;
            //ViewBag.opencode = lotteryResult.Opencode.Replace(","," ");
            //ViewBag.nextExpect = lotteryResult.NextExpect;
            //ViewBag.nextOpenTime = lotteryResult.NextOpenTime;
            ViewBag.lotteryType = "广东快乐十分";
            ViewBag.expect = "--------";
            ViewBag.opencode = "-- -- -- -- -- -- -- --";
            ViewBag.nextExpect = "--------";
            return View();
        }
        //广东11选5
        public ActionResult gd11x5()
        {
            //LotteryResult lotteryResult = GetLastLotteryResultByType("广东11选5");
            //if (lotteryResult == null) return View();
            //ViewBag.lotteryType = lotteryResult.LotteryType;
            //ViewBag.expect = lotteryResult.Expect;
            //ViewBag.openttime = lotteryResult.Opentime;
            //ViewBag.opencode = lotteryResult.Opencode.Replace(",", " ");
            //ViewBag.nextExpect = lotteryResult.NextExpect;
            //ViewBag.nextOpenTime = lotteryResult.NextOpenTime;
            ViewBag.lotteryType = "广东11选5";
            ViewBag.expect = "--------";
            ViewBag.opencode = "-- -- -- -- --";
            ViewBag.nextExpect = "--------";
            return View();
        }
        //重庆时时彩
        public ActionResult ssc()
        {
            //LotteryResult lotteryResult = GetLastLotteryResultByType("重庆时时彩");
            //if (lotteryResult == null) return View();
            //ViewBag.lotteryType = lotteryResult.LotteryType;
            //ViewBag.expect = lotteryResult.Expect;
            //ViewBag.openttime = lotteryResult.Opentime;
            //ViewBag.opencode = lotteryResult.Opencode.Replace(",", " ");
            //ViewBag.nextExpect = lotteryResult.NextExpect;
            //ViewBag.nextOpenTime = lotteryResult.NextOpenTime;
            ViewBag.lotteryType = "重庆时时彩";
            ViewBag.expect = "-----------";
            ViewBag.opencode = "- - - - -";
            ViewBag.nextExpect = "-----------";
            return View();
        }
        //百家乐三公
        public ActionResult bjlsg()
        {
            LotteryResult lotteryResult = GetLastLotteryResultByType("百家乐三公");
            if (lotteryResult == null) return View();
            ViewBag.lotteryType = lotteryResult.LotteryType;
            ViewBag.expect = lotteryResult.Expect;
            ViewBag.openttime = lotteryResult.Opentime;
            ViewBag.opencode = lotteryResult.Opencode.Replace(",", " ");
            ViewBag.nextExpect = lotteryResult.NextExpect;
            ViewBag.nextOpenTime = lotteryResult.NextOpenTime;
            return View();
        }
        //六合彩
        public ActionResult lhc()
        {
            //LotteryResult lotteryResult = GetLastLotteryResultByType("六合彩");
            //if (lotteryResult == null) return View();
            //ViewBag.lotteryType = lotteryResult.LotteryType;
            //ViewBag.expect = lotteryResult.Expect;
            //ViewBag.openttime = lotteryResult.Opentime;
            //ViewBag.opencode = lotteryResult.Opencode.Replace(",", " ");
            //ViewBag.nextExpect = lotteryResult.NextExpect;
            //ViewBag.nextOpenTime = lotteryResult.NextOpenTime;
            ViewBag.lotteryType = "六合彩";
            ViewBag.expect = "------";
            ViewBag.opencode = "-- -- -- -- -- -- ＋ --";
            ViewBag.nextExpect = "------";
            return View();
        }
        //根据彩票类型获取最新的结果
        public LotteryResult GetLastLotteryResultByType(string lotteryType)
        {
            try
            {
                int totalCount = 0;
                var lotteryResult = lrService.LoadPageEntities(1, 1, out totalCount, s => s.LotteryType.Equals(lotteryType), s => s.Expect, false).FirstOrDefault();
                return lotteryResult;
            }
            catch (Exception ex)
            {

                throw new ApplicationException("获取彩票数据出错，原因：" + ex.Message);
            }
           
        }
        //获取距离当前开奖时间的秒数
        public int GetCountDownTimes()
        {
            try
            {
                string ResultType = Request["lotteryType"];
                LotteryResult lotteryResult = GetLastLotteryResultByType(ResultType);
                int countDown = (int)(lotteryResult.NextOpenTime.Value - DateTime.Now).TotalSeconds;
                return countDown;
            }
            catch (Exception ex)
            {

                throw new ApplicationException("获取开奖倒计时的时间出错，原因："+ex.Message);
            }
        }


        List<LotteryResult> VirExcepts = new List<LotteryResult>();
        ResultProcess resP = new ResultProcess();
        [HttpPost]
        public JsonResult GetNextData(string lotteryType)
        {
            try
            {
                LotteryResult lotteryResult = GetLastLotteryResultByType(lotteryType);
                if (lotteryResult == null) return Json(null);
                int countDown = (int)(lotteryResult.NextOpenTime.Value - DateTime.Now).TotalSeconds;
                LotteryResult retRes = new LotteryResult();
                if (countDown <= 0 && (lotteryType == "重庆时时彩"||lotteryType == "广东快乐十分"||lotteryType == "广东11选5"))
                {//为保证用户可以继续下注，到开奖时间后先虚拟下一期出来
                    string VirNextExpect = "";
                    DateTime VirNextOpenTime = DateTime.Now ;
                    switch (lotteryType)
                    {
                        case "重庆时时彩":
                            VirNextExpect = resP.cqsscNextExpect(lotteryResult.NextExpect);
                            VirNextOpenTime = resP.cqsscNextOpenTime(lotteryResult.NextOpenTime.Value);
                            break;
                        case "广东快乐十分":
                            VirNextExpect = resP.gdklsfNextExpect(lotteryResult.NextExpect);
                            VirNextOpenTime = resP.gdklsfNextOpenTime(lotteryResult.NextOpenTime.Value);
                            break;
                        case "广东11选5":
                            VirNextExpect = resP.gd11x5NextExpect(lotteryResult.NextExpect);
                            VirNextOpenTime = resP.gd11x5NextOpenTime(lotteryResult.NextOpenTime.Value);
                            break;
                        default:
                            break;
                    }
                    LotteryResult VirExcept = VirExcepts.Where(s => s.Expect.Equals(lotteryResult.NextExpect) && s.LotteryType.Equals(lotteryType)).FirstOrDefault();
                    if (VirExcept == null)
                    {
                        VirExcept = new LotteryResult();
                        VirExcept.LotteryType = lotteryType;
                        VirExcept.Expect = lotteryResult.NextExpect;
                        VirExcept.NextExpect = VirNextExpect;
                        VirExcept.NextOpenTime = VirNextOpenTime;
                        VirExcepts.Add(VirExcept);
                    }
                    retRes = VirExcept;
                    VirExcepts.RemoveAll(s => s.Expect != lotteryResult.NextExpect && s.LotteryType.Equals(lotteryType)); 
                }
                else
                {
                    retRes = lotteryResult;
                }
                if (retRes.LotteryType.Equals("六合彩"))
                    retRes.Opencode = resP.ConvertlhcNoBose(retRes.Opencode);
                var retVal = new
                {
                    Expect = retRes.Expect,
                    LotteryType = retRes.LotteryType,
                    OpenCode = !string.IsNullOrEmpty(retRes.Opencode) ? retRes.Opencode.Replace(',', ' '):"",
                    NextExpect = retRes.NextExpect,
                    Rema = (int)(retRes.NextOpenTime.Value - DateTime.Now).TotalSeconds
                };
                return Json(retVal, JsonRequestBehavior.AllowGet);
                
            }
            catch (Exception ex)
            {

                throw new ApplicationException("获取开奖倒计时的时间出错，原因：" + ex.Message);
            }
        }


        public ActionResult Prepay()
        {
            //要校验用户是否登陆
            if (!User.Identity.IsAuthenticated) return RedirectToAction("Index", "Login");
            string selectNumbers = Request["selectNumbers"];
            if (selectNumbers == "")
            {
                throw new Exception("注数不能为空！");
            }
            string[] selNum = selectNumbers.Split(',');
            StringBuilder html = new StringBuilder();
            for (int i = 0; i < selNum.Length; i++)
            {
                string[] sn = selNum[i].Split('|');
                if(sn.Length==2)
                    html.AppendFormat(@"<a href='javascript:;' class='selected' style='font-size:9px'>{0}{1}</a>", sn[0], sn[1]);
            }
            ViewBag.selectNumbers = Request["selectNumbers"];
            ViewBag.count = selNum.Length - 1;
            ViewBag.html = html.ToString();
            ViewBag.lotteryType = Request["lotteryType"];
            ViewBag.expect = "------";
            ViewBag.Opencode = "-- -- -- -- --";
            ViewBag.nextExpect = "------";
            UserInfo user = new UserInfoService().LoadEntities(s => s.Name == HttpContext.User.Identity.Name).FirstOrDefault();
            if (user != null)
            {
                ViewBag.blanace = user.Account.Where(s => s.UserId == user.id).FirstOrDefault().Balance;
            }
            else
            {
                ViewBag.blanace = 0.00;
            }
            
            return View();
        }
        public string Pay()
        {
            
            string payCount = Request["payCount"];//倍数
            string selectNumbers = Request["selectNumbers"];//下注的信息
            string lotteryType = Request["lotteryType"];//类型
            string expect = Request["expect"];//期号
            //string Opencode = Request["Opencode"];//开奖号
            string[] selNum = selectNumbers.Split(',');

            string currUserName = HttpContext.User.Identity.Name;
            UserInfo user = userService.LoadEntities(s => s.Name == currUserName && (!s.DelFlag.HasValue || !s.DelFlag == false)).FirstOrDefault();
            if (user == null) throw new ApplicationException("用户信息不存在！");
            Account userAccount = new AccountService().LoadEntities(s => s.UserId == user.id).FirstOrDefault();
            if (userAccount == null) throw new ApplicationException("用户账户信息不存在！");
            var balance = userAccount.Balance;
            if (balance < (selNum.Length - 1) * int.Parse(payCount))
            {
                return "余额不足，请先充值";
            }
            BetRecord br = new BetRecord();
            try
            {
                
                br.Expect = expect;
                br.BetTime = DateTime.Now;
                br.BetMoney = (selNum.Length - 1) * int.Parse(payCount);
                br.BetCount = int.Parse(payCount);
                br.LotteryState = "未开奖";
                br.BetUser = HttpContext.User.Identity.Name;
                br.LotteryType = lotteryType;
                brService.AddEntity(br);
                for (int i = 0; i < selNum.Length; i++)
                {
                    string[] sn = selNum[i].Split('|');
                    if (sn[0].ToString().Equals("特码"))
                        sn[0] = "第7球";
                    if (sn.Length == 2)
                    {
                        BetChildRecord brChild = new BetChildRecord();
                        brChild.BetRecordId = br.Id;
                        brChild.BetBallNo = int.Parse(sn[0].ToString().TrimStart('第').TrimEnd('球'));
                        brChild.BetInfo = sn[1];
                        brChild.BetType = GetBetType(brChild.BetInfo);
                        brChild.Odds = new CalculateOdds(lotteryType, sn[1]).GetOdds();
                        brChild.BetMoney = decimal.Parse(payCount);
                        brChild.LotteryType = lotteryType;
                        brChild.Expect = expect;
                        brChildService.AddEntityNoCommit(brChild);
                    }
                }
                userAccount.Balance = userAccount.Balance - br.BetMoney.Value;
                new AccountService().UpdateEntityNoCommit(userAccount);
                DbService dbservice = new DbService();
                dbservice.Savechange();
                return "ok";
            }
            catch (Exception ex)
            {
                BetRecord inserBr = brService.LoadEntities(s => s.Id == br.Id).FirstOrDefault();
                if (inserBr != null)
                {
                    brService.DeleteEntity(inserBr);
                }
                throw new Exception("投注出错，原因："+ex.Message);
            }
            
        }
       
        private string GetBetType(string BetInfo)
        {
            int temp = 0;
            if (BetInfo.Equals("单") || BetInfo.Equals("双"))
                return "单双";
            else if (BetInfo.Equals("大") || BetInfo.Equals("小"))
                return "大小";
            else if(int.TryParse(BetInfo,out temp))
                return "数字";
            return "";

        }
        public ActionResult gd11x5Number()
        {
            string number = Request["number"];
           ViewBag.number = number;
           LotteryResult lotteryResult = GetLastLotteryResultByType("广东11选5");
           if (lotteryResult == null) return View();
           ViewBag.lotteryType = lotteryResult.LotteryType;
           ViewBag.expect = lotteryResult.Expect;
           ViewBag.openttime = lotteryResult.Opentime;
           ViewBag.opencode = lotteryResult.Opencode.Replace(",", " ");
           ViewBag.nextExpect = lotteryResult.NextExpect;
           ViewBag.nextOpenTime = lotteryResult.NextOpenTime;
            return View();
        }
        public ActionResult klsfNumber()
        {
            string number = Request["number"];
            ViewBag.number = number;
            LotteryResult lotteryResult = GetLastLotteryResultByType("广东快乐十分");
            if (lotteryResult == null) return View();
            ViewBag.lotteryType = lotteryResult.LotteryType;
            ViewBag.expect = lotteryResult.Expect;
            ViewBag.openttime = lotteryResult.Opentime;
            ViewBag.opencode = lotteryResult.Opencode.Replace(",", " ");
            ViewBag.nextExpect = lotteryResult.NextExpect;
            ViewBag.nextOpenTime = lotteryResult.NextOpenTime;
            return View();
        }
        public ActionResult sscNumber()
        {

            LotteryResult lotteryResult = GetLastLotteryResultByType("重庆时时彩");
            if (lotteryResult == null) return View();
            ViewBag.lotteryType = lotteryResult.LotteryType;
            ViewBag.expect = lotteryResult.Expect;
            ViewBag.openttime = lotteryResult.Opentime;
            ViewBag.opencode = lotteryResult.Opencode.Replace(",", " ");
            ViewBag.nextExpect = lotteryResult.NextExpect;
            ViewBag.nextOpenTime = lotteryResult.NextOpenTime;
            return View();
        }
      
      

    }
}
