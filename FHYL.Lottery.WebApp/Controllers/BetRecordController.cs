using FHYL.Lottery.BLL;
using FHYL.Lottery.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace FHYL.Lottery.WebApp.Controllers
{
    public class BetRecordController : Controller
    {
        //
        // GET: /BetRecord/
        LotteryResultService lrService = new LotteryResultService();
        BetRecordService brService = new BetRecordService();
        BetChildRecordService brChildService = new BetChildRecordService();
        public ActionResult Index()
        {
            string lotteryType = Request["lotteryType"];
            ViewBag.lotteryType = lotteryType;
            return View();
        }
        //获取当前用户的投注记录
        public ActionResult GetBetRecord()
        {
            //要校验用户是否登陆
            if (!User.Identity.IsAuthenticated) return RedirectToAction("Index", "Login");
            string lotteryType = Request["lotteryType"];
            string userName = HttpContext.User.Identity.Name;
            List<BetRecord> betRecordList = new List<BetRecord>();
            if (lotteryType == "all")
            {
                betRecordList = brService.LoadEntities(s => s.BetUser.Equals(userName)).ToList();

            }
            else if (lotteryType == "win")
            {
                betRecordList = brService.LoadEntities(s => s.BetUser.Equals(userName) && s.TotalWinOrLose.HasValue).ToList();
             }
            else
            {
                betRecordList = brService.LoadEntities(s => s.LotteryType.Equals(lotteryType) && s.BetUser.Equals(userName)).OrderByDescending(s => s.BetTime).ToList();
            }
           
            
          return Json(betRecordList, JsonRequestBehavior.AllowGet);

        }
        
        public ActionResult BetRecordDetail()
        {
            ViewBag.parentId = Request["parentId"];
            var id = int.Parse(Request["parentId"]);
            BetRecord betRecord = brService.LoadEntities(s=>s.Id==id).FirstOrDefault();
            ViewBag.BetMoney= betRecord.BetMoney;
            ViewBag.BetCount = betRecord.BetCount;
            ViewBag.LotteryState = betRecord.LotteryState;
            LotteryResult lr = lrService.LoadEntities(s=>s.LotteryType == betRecord.LotteryType && s.Expect == betRecord.Expect).FirstOrDefault();
            ViewBag.lotteryType = betRecord.LotteryType;
            ViewBag.expect = betRecord.Expect;
            if (lr == null)
            {
                ViewBag.Opencode = "未开奖";
            }
            else
            {
                ViewBag.Opencode = lr.Opencode;
            }
            return View();
            
        }
        public ActionResult GetBetChildRecordList()
        {
            int parentId = int.Parse(Request["parentId"]);
            var betChildRecordList = brChildService.LoadEntities(s => s.BetRecordId == parentId).ToList();
            return Json(betChildRecordList,JsonRequestBehavior.AllowGet);
        }
    }
}
