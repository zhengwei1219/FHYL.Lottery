using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FHYL.Lottery.LotteryLibrary
{
    public class ResultProcess
    {
        /// <summary>
        /// 获取下一期期号
        /// </summary>
        /// <returns></returns>
        public string GetNextExpect(string LotteryType, string CurrentExpect)
        {
            switch (LotteryType)
            {
                case "重庆时时彩":
                    return cqsscNextExpect(CurrentExpect);
                case "广东快乐十分":
                    return gdklsfNextExpect(CurrentExpect);
                case "广东11选5":
                    return gd11x5NextExpect(CurrentExpect);
                //case "六合彩":
                //    return lhcNextExpect(CurrentExpect);
                default:
                    break;
            }
            return "";
        }
        /// <summary>
        /// 重庆时时彩下一期期号计算方法
        /// </summary>
        /// <param name="CurrentExpect"></param>
        /// <returns></returns>
        public string cqsscNextExpect(string CurrentExpect)
        {

            //一天120期，前面8位是年月日，后面3位是期号
            string Currday = CurrentExpect.Substring(0, 8);
            string Currqihao = CurrentExpect.Substring(8, 3);
            DateTime CurrdayDate = DateTime.ParseExact(Currday, "yyyyMMdd", null);
            int CurrqihaoInt = int.Parse(Currqihao);

            if (CurrqihaoInt == 120)
                return string.Format("{0}{1}", CurrdayDate.AddDays(1).ToString("yyyyMMdd"), "001");
            else
                return string.Format("{0}{1}", Currday, (CurrqihaoInt + 1).ToString().PadLeft(3, '0'));
        }
        /// <summary>
        /// 重庆时时彩下一期期号计算方法
        /// </summary>
        /// <param name="CurrentExpect"></param>
        /// <returns></returns>
        public string gdklsfNextExpect(string CurrentExpect)
        {
            //一天84期，前面8位是年月日，后面2位是期号
            string Currday = CurrentExpect.Substring(0, 8);
            string Currqihao = CurrentExpect.Substring(8, 2);
            DateTime CurrdayDate = DateTime.ParseExact(Currday, "yyyyMMdd", null);
            int CurrqihaoInt = int.Parse(Currqihao);
            if (CurrqihaoInt == 84)
                return string.Format("{0}{1}", CurrdayDate.AddDays(1).ToString("yyyyMMdd"), "01");
            else
                return string.Format("{0}{1}", Currday, (CurrqihaoInt + 1).ToString().PadLeft(2, '0'));
        }
        /// <summary>
        /// 广东11选五下一期期号计算方法
        /// </summary>
        /// <param name="CurrentExpect"></param>
        /// <returns></returns>
        public string gd11x5NextExpect(string CurrentExpect)
        {
            //一天84期，前面6位是年月日，后面2位是期号
            string Currday = CurrentExpect.Substring(0, 6);
            string Currqihao = CurrentExpect.Substring(6, 2);
            DateTime CurrdayDate = DateTime.ParseExact(Currday, "yyMMdd", null);
            int CurrqihaoInt = int.Parse(Currqihao);
            if (CurrqihaoInt == 84)
                return string.Format("{0}{1}", CurrdayDate.AddDays(1).ToString("yyMMdd"), "01");
            else
                return string.Format("{0}{1}", Currday, (CurrqihaoInt + 1).ToString().PadLeft(2, '0'));
        }
        /// <summary>
        /// 六合彩下一期期号计算方法
        /// </summary>
        /// <param name="CurrentExpect"></param>
        /// <returns></returns>
        public string lhcNextExpect(string CurrentExpect)
        {
            //当前期号+1
            return (int.Parse(CurrentExpect) + 1).ToString();
        }


        /// <summary>
        /// 获取下一期开奖时间
        /// </summary>
        /// <returns></returns>
        public DateTime GetNextOpenTime(string LotteryType, DateTime CurrentOpenTime)
        {
            switch (LotteryType)
            {
                case "重庆时时彩":
                    return cqsscNextOpenTime(CurrentOpenTime);
                case "广东快乐十分":
                    return gdklsfNextOpenTime(CurrentOpenTime);
                case "广东11选5":
                    return gd11x5NextOpenTime(CurrentOpenTime);
                //case "六合彩":
                //    return GetHttpInfoForlhc.GetNextOpenTime();
                default:
                    break;
            }
            return DateTime.MaxValue;
        }

        public DateTime cqsscNextOpenTime(DateTime CurrentOpenTime)
        {
            //规则：24:00-2:00(24期)5分钟一期  10:00-22:00（72期）10分钟一期，22:00-24:00（24期）5分钟一期
            //当前时间取小时分钟
            int CurrHour = CurrentOpenTime.Hour;
            int CurrMin = CurrentOpenTime.Minute;
            //1:55开奖后一直到10：00才开奖
            if (CurrHour == 1 && CurrMin == 55)
                return new DateTime(CurrentOpenTime.Year, CurrentOpenTime.Month, CurrentOpenTime.Day, 10, 0, 0);
            else if (CurrHour >= 22 || CurrHour < 2)
                return CurrentOpenTime.AddMinutes(5);
            else if (CurrHour < 22 && CurrHour >= 10)
                return CurrentOpenTime.AddMinutes(10);
            else
                return DateTime.MaxValue;
        }

        public DateTime gdklsfNextOpenTime(DateTime CurrentOpenTime)
        {
            //规则：9:10-23:00(84期)10分钟一期
            //当前时间取小时分钟
            int CurrHour = CurrentOpenTime.Hour;
            int CurrMin = CurrentOpenTime.Minute;
            //23:00开奖后一直到第二天9：10才开奖
            if (CurrHour == 23 && CurrMin == 00)
                return CurrentOpenTime.AddHours(10).AddMinutes(10);
            else
                return CurrentOpenTime.AddMinutes(10);
        }
        public DateTime gd11x5NextOpenTime(DateTime CurrentOpenTime)
        {
            //规则：9:10-23:00(84期)10分钟一期
            //当前时间取小时分钟
            int CurrHour = CurrentOpenTime.Hour;
            int CurrMin = CurrentOpenTime.Minute;
            //23:00开奖后一直到第二天9：10才开奖
            if (CurrHour == 23 && CurrMin == 00)
                return CurrentOpenTime.AddHours(10).AddMinutes(10);
            else
                return CurrentOpenTime.AddMinutes(10);
        }
        string[] redball = new string[] { "01", "02", "08", "07", "12", "13", "18", "19", "23", "24", "29", "30", "34", "35", "40", "45", "46" };
        string[] greenball = new string[] { "06", "05", "11", "17", "16", "22", "21", "28", "27", "33", "32", "39", "38", "49", "44", "43" };
        string[] blueball = new string[] { "04", "03", "10", "09", "15", "14", "20", "26", "25", "37", "36", "31", "42", "41", "48", "47" };
            
        /// <summary>
        /// 六合彩开奖号码增加波色显示（号码组转换）
        /// </summary>
        /// <returns></returns>
        public string ConvertlhcNoBose(string orgNo)
        {
            string[] orgArray = orgNo.Split(',');
            string retVal = "";
            for (int i = 0; i < orgArray.Length; i++)
            {
                if (i == orgArray.Length - 1)
                    retVal += "<font color='black'>＋</font>";
                if (redball.Contains(orgArray[i]))
                    retVal += string.Format("<font color='red'>{0}</font>,", orgArray[i]);
                else if (greenball.Contains(orgArray[i]))
                    retVal += string.Format("<font color='green'>{0}</font>,", orgArray[i]);
                else
                    retVal += string.Format("<font color='blue'>{0}</font>,", orgArray[i]);
            }
            return retVal.TrimEnd(',');
        }
        /// <summary>
        /// 六合彩开奖号码增加波色显示（单个转换）
        /// </summary>
        /// <param name="orgNo"></param>
        /// <returns></returns>
        public string ConvertlhcNoBoseForOne(string orgNo)
        {
            if (redball.Contains(orgNo))
                return string.Format("<font color='red'>{0}</font>", orgNo);
            else if (greenball.Contains(orgNo))
                return string.Format("<font color='green'>{0}</font>", orgNo);
            else
                return string.Format("<font color='blue'>{0}</font>", orgNo);
        }

    }
}
