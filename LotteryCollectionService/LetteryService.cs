using FHYL.Lottery.BLL;
using FHYL.Lottery.LotteryLibrary;
using FHYL.Lottery.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Xml.Linq;

namespace LotteryCollectionService
{
   public class LetteryService
    {
        //public static void Start()
        //{
        //    ThreadStart start = new ThreadStart(GetLetteryInfo);
        //    Thread th = new Thread(start);
        //    th.IsBackground = true;
        //    th.Start();
        //}

        public static void GetLetteryInfo()
        {

            try
            {
                string path = System.AppDomain.CurrentDomain.BaseDirectory;
                string fullPath = path + "LotteryWebsite.xml";
                XDocument doc = XDocument.Load(fullPath);
                var website = doc.Descendants("webSite").Select(p => new { lotteryType = p.Element("lotteryType").Value, url = p.Element("url").Value }).ToList();
                BetLotteryProcess betprocess = new BetLotteryProcess();
                ResultProcess resProcess = new ResultProcess();
                foreach (var item in website)
                {
                    string html = HttpGet(item.url, Encoding.UTF8);
                    LotteryResultService lrService = new LotteryResultService();
                    Dictionary<string, JObject> dicreds = DeserializeStringToDictionary<string, JObject>(html);
                    foreach (var dictionary in dicreds)
                    {
                        LotteryResult existLotteryResult = lrService.LoadEntities(s => s.LotteryType == item.lotteryType && s.Expect == dictionary.Key ).FirstOrDefault();
                        if (existLotteryResult != null)
                        {
                            continue;
                        }
                        JObject lottery = dictionary.Value;
                        LotteryResult lotteryResult = new LotteryResult();
                        lotteryResult.LotteryType = item.lotteryType;
                        lotteryResult.Expect = dictionary.Key;
                        lotteryResult.Opencode = lottery["number"].ToString();
                        //开奖日期不能用接口反回的时间，不准确
                        
                        //if (lotteryResult.LotteryType.Equals("六合彩"))
                            lotteryResult.Opentime = GetRealOpenTime(lotteryResult.LotteryType, DateTime.Parse(lottery["dateline"].ToString()));
                            lotteryResult.Opentime = lotteryResult.Opentime.Value.AddSeconds(0 - lotteryResult.Opentime.Value.Second);
                        //else
                        //{
                        //    LotteryResult beforeResult = lrService.LoadEntities(s => s.LotteryType == item.lotteryType).OrderByDescending(s => s.Expect).FirstOrDefault();
                        //    if (beforeResult == null)
                        //        lotteryResult.Opentime = GetRealOpenTime(lotteryResult.LotteryType, DateTime.Parse(lottery["dateline"].ToString()));
                        //    else
                        //        lotteryResult.Opentime = beforeResult.NextOpenTime;
                        //}
                            if (lotteryResult.LotteryType.Equals("六合彩"))
                                GetHttpInfoForlhc.GetlhcInfo(lotteryResult);
                            else
                            {
                                lotteryResult.NextExpect = resProcess.GetNextExpect(item.lotteryType, lotteryResult.Expect);
                                lotteryResult.NextOpenTime =resProcess.GetNextOpenTime(item.lotteryType, lotteryResult.Opentime.Value);
                            }
                        lrService.AddEntity(lotteryResult);
                        betprocess.ProcessBet(lotteryResult);
                    }
                }

            }
            catch (Exception ex)
            {

                throw;
            }
            
        }
        private static DateTime GetRealOpenTime(string lotteryType,DateTime OpenTime)
        {
            switch (lotteryType)
            {
                case "重庆时时彩":
                    return cqsscRealOpenTime(OpenTime);
                case "广东快乐十分":
                    return gzklsfRealOpenTime(OpenTime);
                case "广东11选5":
                    return gzklsfRealOpenTime(OpenTime);
                case "六合彩":
                    return lhcRealOpenTime(OpenTime);
                default:
                    return OpenTime;
            }
        }

        private static DateTime cqsscRealOpenTime(DateTime OpenTime)
        {
            int minval = OpenTime.Minute;
            //if (minval % 10 >= 5)
                return OpenTime.AddMinutes(0 - (minval % 5));
            //else
            //    return OpenTime.AddMinutes(0 - (minval % 10));
        }
        private static DateTime gzklsfRealOpenTime(DateTime OpenTime)
        {
            int minval = OpenTime.Minute;
            //if (minval % 10 >= 5)
            return OpenTime.AddMinutes(0 - (minval % 10));
            //else
            //    return OpenTime.AddMinutes(0 - (minval % 10));
        }
        private static DateTime lhcRealOpenTime(DateTime OpenTime)
        {
            int minval = OpenTime.Minute;
            //if (minval % 10 >= 5)
            return OpenTime.AddMinutes(0 - (minval % 30));
            //else
            //    return OpenTime.AddMinutes(0 - (minval % 10));
        }
       /// <summary>
        /// 将json字符串反序列化为字典类型
         /// </summary>
        /// <typeparam name="TKey">字典key</typeparam>
        /// <typeparam name="TValue">字典value</typeparam>
        /// <param name="jsonStr">json字符串</param>
       /// <returns>字典数据</returns>
         public static Dictionary<TKey, TValue> DeserializeStringToDictionary<TKey, TValue>(string jsonStr)
         {
             if (string.IsNullOrEmpty(jsonStr))
                return new Dictionary<TKey, TValue>();
 
            Dictionary<TKey, TValue> jsonDict = JsonConvert.DeserializeObject<Dictionary<TKey, TValue>>(jsonStr);
 
             return jsonDict;
 
         }
        public static string HttpGet(string url, Encoding enc)
        {
            Thread.Sleep(2000);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Timeout = 10000;//设置10秒超时
            request.Proxy = null;
            request.Method = "GET";
            request.ContentType = "application/x-www-from-urlencoded";
            WebResponse response = request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream(), enc);
            StringBuilder sb = new StringBuilder();
            sb.Append(reader.ReadToEnd());
            reader.Close();
            reader.Dispose();
            response.Close();
            return sb.ToString();
        }
      
    }
}
