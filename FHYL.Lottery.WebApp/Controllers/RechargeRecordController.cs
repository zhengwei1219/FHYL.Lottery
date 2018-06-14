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
    public class RechargeRecordController : Controller
    {
        //
        // GET: /RechargeRecord/
        RechargeRecordService rrService = new RechargeRecordService();
        UserInfoService uService = new UserInfoService();
        public ActionResult Index()
        {

            return View();
        }
        public string GetRechargeRecordList()
        {
            string userName = User.Identity.Name;
            UserInfo user = uService.LoadEntities(s => s.Name == userName).FirstOrDefault();
            StringBuilder html = new StringBuilder();
            if (user != null)
            {                

                foreach ( RechargeRecord rr in user.RechargeRecord)
                {
                    html.AppendFormat("<tr><th>{0}</th><th>{1}</th><th>{2:yy年MM月dd}</th></tr>", rr.RechargeBy, rr.RechargeMoney, rr.RechargeDate);
                }
                
            }
            return html.ToString();
            
        }

    }
}
