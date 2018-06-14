using FHYL.Lottery.BLL;
using FHYL.Lottery.LotteryLibrary;
using FHYL.Lottery.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace FHYL.Lottery.WebApp.Controllers
{
    public class LotteryResultController : Controller
    {
        //
        // GET: /LetteryResult/
        LotteryResultService lrService = new LotteryResultService();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetLotteryResultHtml()
        {
            string lotteryType = Request["lotteryType"];
            int pageIndex =1;
            int pageSize =10;
            int totalCount =0;
            List<LotteryResult> lotteryResult = lrService.LoadPageEntities(pageIndex, pageSize,out totalCount, s => s.LotteryType.Equals(lotteryType), s => s.Opentime, false).ToList();
            switch (lotteryType)
            {
                case "广东快乐十分":
                    return HappyTenMinutesInGuangdongHtml(lotteryResult);
                case "广东11选5":
                    return ElevenSelectFiveInGuangdongHtml(lotteryResult);
                case "重庆时时彩":
                    return TimeLotteryInChongqingHtml(lotteryResult);
                case "六合彩":
                    return lhcHtml(lotteryResult);
                default :
                    return HappyTenMinutesInGuangdongHtml(lotteryResult);
            }
            
        }

        private ActionResult TimeLotteryInChongqingHtml(List<LotteryResult> lotteryResult)
        {
            StringBuilder numberHtml = new StringBuilder();
            numberHtml.Append("<thead><tr><th>期号</th><th>万</th><th>千</th><th>佰</th><th>拾</th><th>个</th></tr></thead>");
            numberHtml.Append("<tbody>");
            StringBuilder bigOrSmallHtml = new StringBuilder();
            bigOrSmallHtml.Append("<thead><tr><th>期号</th><th>万</th><th>千</th><th>佰</th><th>拾</th><th>个</th></tr></thead>");
            bigOrSmallHtml.Append("<tbody>");
            StringBuilder evenOrOddHtml = new StringBuilder();
            evenOrOddHtml.Append("<thead><tr><th>期号</th><th>万</th><th>千</th><th>佰</th><th>拾</th><th>个</th></tr></thead>");
            evenOrOddHtml.Append("<tbody>");
            StringBuilder totalHtml = new StringBuilder();
            totalHtml.Append("<thead><tr><th>期号</th><th>总和</th><th>大小</th><th>单双</th><th>龙虎</th></tr></thead>");
            totalHtml.Append("<tbody>");
            foreach (var item in lotteryResult)
            {
                
                string expect = item.Expect.Substring(8, 3);
                string[] code = item.Opencode.Split(',');
                int sum = int.Parse(code[0])+int.Parse(code[1])+int.Parse(code[2])+int.Parse(code[3])+int.Parse(code[4]);
                numberHtml.AppendFormat("<tr><th>{0}</th><th>{1}</th><th>{2}</th><th>{3}</th><th>{4}</th><th>{5}</th></tr>", expect, code[0], code[1], code[2], code[3], code[4]);
                bigOrSmallHtml.AppendFormat("<tr><th>{0}</th><th>{1}</th><th>{2}</th><th>{3}</th><th>{4}</th><th>{5}</th></tr>", expect, int.Parse(code[0]) >= 5 ? "大" : "小", int.Parse(code[1]) >= 5 ? "大" : "小", int.Parse(code[2]) >= 5 ? "大" : "小", int.Parse(code[3]) >= 5 ? "大" : "小", int.Parse(code[4]) >= 5 ? "大" : "小");
                evenOrOddHtml.AppendFormat("<tr><th>{0}</th><th>{1}</th><th>{2}</th><th>{3}</th><th>{4}</th><th>{5}</th></tr>", expect, int.Parse(code[0]) % 2 == 0 ? "双" : "单", int.Parse(code[1]) % 2 == 0 ? "双" : "单", int.Parse(code[2]) % 2 == 0 ? "双" : "单", int.Parse(code[3]) % 2 == 0 ? "双" : "单", int.Parse(code[4]) % 2 == 0 ? "双" : "单");
               
                string totalMark = "和";
                if(int.Parse(code[0])>int.Parse(code[4]))
                {
                totalMark = "龙";
                }
                if(int.Parse(code[0])<int.Parse(code[4]))
                {
                totalMark = "虎";
                }
                totalHtml.AppendFormat("<tr><th>{0}</th><th>{1}</th><th>{2}</th><th>{3}</th><th>{4}</th></tr>", expect, sum, sum >= 23 ? "大" : "小", sum % 2 == 0 ? "双" : "单", totalMark);
            }
            numberHtml.Append("</tbody>");
            bigOrSmallHtml.Append("</tbody>");
            evenOrOddHtml.Append("</tbody>");
            totalHtml.Append("</tbody>");
            return Json(new { number = numberHtml.ToString(), bigorSmall = bigOrSmallHtml.ToString(), evenOrOdd = evenOrOddHtml.ToString(), total = totalHtml.ToString()});
        }

        private ActionResult ElevenSelectFiveInGuangdongHtml(List<LotteryResult> lotteryResult)
        {
            StringBuilder numberHtml = new StringBuilder();
            numberHtml.Append("<thead><tr><th>期号</th><th>一</th><th>二</th><th>三</th><th>四</th><th>五</th></tr></thead>");
            numberHtml.Append("<tbody>");
            StringBuilder bigOrSmallHtml = new StringBuilder();
            bigOrSmallHtml.Append("<thead><tr><th>期号</th><th>一</th><th>二</th><th>三</th><th>四</th><th>五</th></tr></thead>");
            bigOrSmallHtml.Append("<tbody>");
            StringBuilder evenOrOddHtml = new StringBuilder();
            evenOrOddHtml.Append("<thead><tr><th>期号</th><th>一</th><th>二</th><th>三</th><th>四</th><th>五</th></tr></thead>");
            evenOrOddHtml.Append("<tbody>");
            //StringBuilder totalHtml = new StringBuilder();
            //totalHtml.Append("<thead><tr><th>期号</th><th>总和</th><th>大小</th><th>单双</th><th>龙虎</th></tr></thead>");
            //totalHtml.Append("<tbody>");
            foreach (var item in lotteryResult)
            {

                string expect = item.Expect.Substring(6, 2);
                string[] code = item.Opencode.Split(',');
                //if (code[0].Equals("**")) code[0] = "01";
                //int sum = int.Parse(code[0]) + int.Parse(code[1]) + int.Parse(code[2]) + int.Parse(code[3]) + int.Parse(code[4]);
                numberHtml.AppendFormat("<tr><th>{0}</th><th>{1}</th><th>{2}</th><th>{3}</th><th>{4}</th><th>{5}</th></tr>", expect, code[0], code[1], code[2], code[3], code[4]);
                bigOrSmallHtml.AppendFormat("<tr><th>{0}</th><th>{1}</th><th>{2}</th><th>{3}</th><th>{4}</th><th>{5}</th></tr>", expect, int.Parse(code[0]) >= 8 ? "大" : "小", int.Parse(code[1]) >= 5 ? "大" : "小", int.Parse(code[2]) >= 5 ? "大" : "小", int.Parse(code[3]) >= 5 ? "大" : "小", int.Parse(code[4]) >= 5 ? "大" : "小");
                evenOrOddHtml.AppendFormat("<tr><th>{0}</th><th>{1}</th><th>{2}</th><th>{3}</th><th>{4}</th><th>{5}</th></tr>", expect, int.Parse(code[0]) % 2 == 0 ? "双" : "单", int.Parse(code[1]) % 2 == 0 ? "双" : "单", int.Parse(code[2]) % 2 == 0 ? "双" : "单", int.Parse(code[3]) % 2 == 0 ? "双" : "单", int.Parse(code[4]) % 2 == 0 ? "双" : "单");

                string totalMark = "和";
                if (int.Parse(code[0]) > int.Parse(code[4]))
                {
                    totalMark = "龙";
                }
                if (int.Parse(code[0]) < int.Parse(code[4]))
                {
                    totalMark = "虎";
                }
                //totalHtml.AppendFormat("<tr><th>{0}</th><th>{1}</th><th>{2}</th><th>{3}</th><th>{4}</th></tr>", expect, sum, sum >= 80 ? "大" : "小", sum % 2 == 0 ? "双" : "单", totalMark);
            }
            numberHtml.Append("</tbody>");
            bigOrSmallHtml.Append("</tbody>");
            evenOrOddHtml.Append("</tbody>");
            //totalHtml.Append("</tbody>");
            return Json(new { number = numberHtml.ToString(), bigorSmall = bigOrSmallHtml.ToString(), evenOrOdd = evenOrOddHtml.ToString(), total = "" });
        }

        private ActionResult HappyTenMinutesInGuangdongHtml(List<LotteryResult> lotteryResult)
        {
            StringBuilder numberHtml = new StringBuilder();
            numberHtml.Append("<thead><tr><th>期号</th><th>一</th><th>二</th><th>三</th><th>四</th><th>五</th><th>六</th><th>七</th><th>八</th></tr></thead>");
            numberHtml.Append("<tbody>");
            StringBuilder bigOrSmallHtml = new StringBuilder();
            bigOrSmallHtml.Append("<thead><tr><th>期号</th><th>一</th><th>二</th><th>三</th><th>四</th><th>五</th><th>六</th><th>七</th><th>八</th></tr></thead>");
            bigOrSmallHtml.Append("<tbody>");
            StringBuilder evenOrOddHtml = new StringBuilder();
            evenOrOddHtml.Append("<thead><tr><th>期号</th><th>一</th><th>二</th><th>三</th><th>四</th><th>五</th><th>六</th><th>七</th><th>八</th></tr></thead>");
            evenOrOddHtml.Append("<tbody>");
            //StringBuilder totalHtml = new StringBuilder();
            //totalHtml.Append("<thead><tr><th>期号</th><th>总和</th><th>大小</th><th>单双</th><th>龙虎</th></tr></thead>");
            //totalHtml.Append("<tbody>");
            foreach (var item in lotteryResult)
            {

                string expect = item.Expect.Substring(8,2);
                string[] code = item.Opencode.Split(',');
                //if (code[0].Equals("**")) code[0] = "01";
               // int sum = int.Parse(code[0]) + int.Parse(code[1]) + int.Parse(code[2]) + int.Parse(code[3]) + int.Parse(code[4]) + int.Parse(code[5]) + int.Parse(code[6]) + int.Parse(code[7]);
                numberHtml.AppendFormat("<tr><th>{0}</th><th>{1}</th><th>{2}</th><th>{3}</th><th>{4}</th><th>{5}</th><th>{6}</th><th>{7}</th><th>{8}</th></tr>", expect, code[0], code[1], code[2], code[3], code[4], code[5], code[6], code[7]);
                bigOrSmallHtml.AppendFormat("<tr><th>{0}</th><th>{1}</th><th>{2}</th><th>{3}</th><th>{4}</th><th>{5}</th><th>{6}</th><th>{7}</th><th>{8}</th></tr>", expect, int.Parse(code[0]) >= 8 ? "大" : "小", int.Parse(code[1]) >= 8 ? "大" : "小", int.Parse(code[2]) >= 8 ? "大" : "小", int.Parse(code[3]) >= 8 ? "大" : "小", int.Parse(code[4]) >= 8 ? "大" : "小", int.Parse(code[5]) >= 8 ? "大" : "小", int.Parse(code[6]) >= 8 ? "大" : "小", int.Parse(code[7]) >= 8 ? "大" : "小");
                evenOrOddHtml.AppendFormat("<tr><th>{0}</th><th>{1}</th><th>{2}</th><th>{3}</th><th>{4}</th><th>{5}</th><th>{6}</th><th>{7}</th><th>{8}</th></tr>", expect, int.Parse(code[0]) % 2 == 0 ? "双" : "单", int.Parse(code[1]) % 2 == 0 ? "双" : "单", int.Parse(code[2]) % 2 == 0 ? "双" : "单", int.Parse(code[3]) % 2 == 0 ? "双" : "单", int.Parse(code[4]) % 2 == 0 ? "双" : "单", int.Parse(code[5]) % 2 == 0 ? "双" : "单", int.Parse(code[6]) % 2 == 0 ? "双" : "单", int.Parse(code[7]) % 2 == 0 ? "双" : "单");

                //string totalMark = "和";
                //if (int.Parse(code[0]) > int.Parse(code[7]))
                //{
                //    totalMark = "龙";
                //}
                //if (int.Parse(code[0]) < int.Parse(code[7]))
                //{
                //    totalMark = "虎";
                //}
                //totalHtml.AppendFormat("<tr><th>{0}</th><th>{1}</th><th>{2}</th><th>{3}</th><th>{4}</th></tr>", expect, sum, sum >= 80 ? "大" : "小", sum % 2 == 0 ? "双" : "单", totalMark);
            }
            numberHtml.Append("</tbody>");
            bigOrSmallHtml.Append("</tbody>");
            evenOrOddHtml.Append("</tbody>");
            //totalHtml.Append("</tbody>");
            return Json(new { number = numberHtml.ToString(), bigorSmall = bigOrSmallHtml.ToString(), evenOrOdd = evenOrOddHtml.ToString(), total =""  });
        }
        ResultProcess Resp = new ResultProcess();
        private ActionResult lhcHtml(List<LotteryResult> lotteryResult)
        {
            StringBuilder numberHtml = new StringBuilder();
            numberHtml.Append("<thead><tr><th>期号</th><th>特码</th><th>大小</th><th>单双</th></thead>");
            numberHtml.Append("<tbody>");
            StringBuilder bigOrSmallHtml = new StringBuilder();
            bigOrSmallHtml.Append("<thead><tr><th>期号</th><th>特码</th><th>大小</th><th>单双</th></thead>");
            bigOrSmallHtml.Append("<tbody>");
            StringBuilder evenOrOddHtml = new StringBuilder();
            evenOrOddHtml.Append("<thead><tr><th>期号</th><th>特码</th><th>大小</th><th>单双</th></thead>");
            evenOrOddHtml.Append("<tbody>");
            //StringBuilder totalHtml = new StringBuilder();
            //totalHtml.Append("<thead><tr><th>期号</th><th>总和</th><th>大小</th><th>单双</th><th>龙虎</th></tr></thead>");
            //totalHtml.Append("<tbody>");
            foreach (var item in lotteryResult)
            {

                string expect = item.Expect.Substring(4, 3);
                string[] code = item.Opencode.Split(',');
                //if (code[0].Equals("**")) code[0] = "01";
                //int sum = int.Parse(code[0]) + int.Parse(code[1]) + int.Parse(code[2]) + int.Parse(code[3]) + int.Parse(code[4]);
                numberHtml.AppendFormat("<tr><th>{0}</th><th>{1}</th><th>{2}</th><th>{3}</th></tr>", expect, Resp.ConvertlhcNoBoseForOne(code[code.Length - 1]), int.Parse(code[code.Length - 1]) >= 25 ? "大" : "小", int.Parse(code[code.Length - 1]) % 2 == 0 ? "双" : "单");
                bigOrSmallHtml.AppendFormat("<tr><th>{0}</th><th>{1}</th><th>{2}</th><th>{3}</th></tr>", expect, Resp.ConvertlhcNoBoseForOne(code[code.Length - 1]), int.Parse(code[code.Length - 1]) >= 25 ? "大" : "小", int.Parse(code[code.Length - 1]) % 2 == 0 ? "双" : "单");
                evenOrOddHtml.AppendFormat("<tr><th>{0}</th><th>{1}</th><th>{2}</th><th>{3}</th></tr>", expect, Resp.ConvertlhcNoBoseForOne(code[code.Length - 1]), int.Parse(code[code.Length - 1]) >= 25 ? "大" : "小", int.Parse(code[code.Length - 1]) % 2 == 0 ? "双" : "单");

                //string totalMark = "和";
                //if (int.Parse(code[0]) > int.Parse(code[4]))
                //{
                //    totalMark = "龙";
                //}
                //if (int.Parse(code[0]) < int.Parse(code[4]))
                //{
                //    totalMark = "虎";
                //}
                //totalHtml.AppendFormat("<tr><th>{0}</th><th>{1}</th><th>{2}</th><th>{3}</th><th>{4}</th></tr>", expect, sum, sum >= 80 ? "大" : "小", sum % 2 == 0 ? "双" : "单", totalMark);
            }
            numberHtml.Append("</tbody>");
            bigOrSmallHtml.Append("</tbody>");
            evenOrOddHtml.Append("</tbody>");
            //totalHtml.Append("</tbody>");
            return Json(new { number = numberHtml.ToString(), bigorSmall = bigOrSmallHtml.ToString(), evenOrOdd = evenOrOddHtml.ToString(), total = "" });
        }

    }
}
