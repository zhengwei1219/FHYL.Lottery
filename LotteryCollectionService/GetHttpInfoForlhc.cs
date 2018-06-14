using FHYL.Lottery.BLL;
using FHYL.Lottery.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LotteryCollectionService
{
    /// <summary>
    /// 网页上抓取六合彩下一期开奖时间
    /// </summary>
    public class GetHttpInfoForlhc
    {

        public static void GetlhcInfo(LotteryResult retObj)
        {
            try
            {

                string openuri = "http://tkkj.cc/";
                WebRequest request = WebRequest.Create(openuri);
                WebResponse response = request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("utf-8"));

                string uriInfo = reader.ReadToEnd();
                //string ExpectmarkStr = "第<font size=\"\" color=\"#ff6600\"><strong>";
                //retObj.Expect = uriInfo.Substring(uriInfo.IndexOf(ExpectmarkStr) + ExpectmarkStr.Length, 3);
                string OpencodemarkStr = "特:";
                string opencodestr = uriInfo.Substring(uriInfo.IndexOf(OpencodemarkStr) -17, 21);
                retObj.Opencode = opencodestr.Replace("-", ",").Replace("特:", ",");
                string NextmarkStr = "开<br/>";
                string NextStr = uriInfo.Substring(uriInfo.IndexOf(NextmarkStr) - 19, 19);
                
                int nextYear = retObj.Opentime.Value.Year;
                if (retObj.NextExpect == "001")
                    nextYear++;
                retObj.NextExpect = nextYear.ToString() + NextStr.Substring(0, 3);
                string NextOpenTimeStr = nextYear.ToString() + "-" + NextStr.Substring(5, 5).Replace("月", "-") + " " + NextStr.Substring(NextStr.Length-5,5);
                retObj.NextOpenTime = DateTime.Parse(NextOpenTimeStr);

            }
            catch (Exception ex)
            {
                throw new Exception("六合彩开奖数据获取失败，原因：" + ex.Message);
            }
        }
        //public static DateTime GetNextOpenTime()
        //{
        //    string openuri = "http://0067333.com/default.aspx?v=0";
        //    WebRequest request = WebRequest.Create(openuri);
        //    WebResponse response = request.GetResponse();
        //    StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("utf-8"));

        //    string uriInfo = reader.ReadToEnd();

        //    string dateTimeNext = uriInfo.Substring(uriInfo.IndexOf("开奖时间:")+5, 16);
        //    return DateTime.Parse(dateTimeNext);

        //}

    }
}
