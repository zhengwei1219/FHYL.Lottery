using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FHYL.Lottery.WebApp
{
    //各种彩票的赔率计算
    public class CalculateOdds
    {
        public string BetNum;
        public string LotteryType;
        public CalculateOdds(string lotteryType,string betNum)
        {
            this.BetNum = betNum;
            this.LotteryType = lotteryType;
        }
        public double? GetOdds()
        {
            double? odds = 0;
            switch (LotteryType)
            { 
                case "广东11选5":
                    odds = GetOddsOfGd11x5();
                    break;
                case "广东快乐十分":
                    odds = GetOddsOfklsf();
                    break;
                case "重庆时时彩":
                    odds = GetOddsOfSsc();
                    break;
                case "六合彩":
                    odds = GetOddsOfLhc();
                    break;
            }
            return odds;
        }
        //六合彩
        private double? GetOddsOfLhc()
        {
            double? odds = 0;
            if (BetNum.IndexOf("号") > 0)
            { //单选球
                odds = 48;
            }
            else
            {
                odds = 1.92;
            }
            return odds;
        }
        //重庆时时彩
        private double? GetOddsOfSsc()
        {
            double? odds = 0;
            if (BetNum.IndexOf("号") > 0)
            { //单选球
                odds = 9;
            }
            else
            {
                odds = 1.92;
            }
            return odds;
        }
        //广东快乐十分
        private double? GetOddsOfklsf()
        {
            double? odds = 0;
            if (BetNum.IndexOf("号") > 0)
            { //单选球
                odds = 18;
            }
            else
            {
                odds = 1.92;
            }
            return odds;
        }
        //广东11选5
        private double? GetOddsOfGd11x5()
        {
            double? odds = 0;
            if (BetNum.IndexOf("号") > 0)
            { //数字
                odds = 10;
            }
            else
            {
                if (BetNum.Equals("大") || BetNum.Equals("单"))
                {
                    odds = 1.9;
                }
                else
                {
                    odds = 1.95;
                }
            }
            return odds;
        }
    }
}