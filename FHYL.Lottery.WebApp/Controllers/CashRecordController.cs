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
    public class CashRecordController : Controller
    {
        //
        // GET: /CashRecord/
        UserInfoService uService = new UserInfoService();
        public ActionResult Index()
        {
            return View();
        }

        public string GetCashRecordList()
        {
            string userName = User.Identity.Name;
            UserInfo user = uService.LoadEntities(s => s.Name == userName).FirstOrDefault();
            StringBuilder html = new StringBuilder();
            if (user != null)
            {

                foreach (CashRecord rr in user.CashRecord)
                {
                    html.AppendFormat("<tr><th>{0}</th><th>{1}</th><th>{2:yy年MM月dd}</th></tr>", rr.CashBy, rr.CashMoney, rr.CashDate);
                }

            }
            return html.ToString();
        }

    }
}
