using FHYL.Lottery.BLL;
using FHYL.Lottery.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotteryCollectionService
{
    /// <summary>
    /// 开奖处理类
    /// </summary>
    public class BetLotteryProcess
    {
        BetChildRecordService betCService = new BetChildRecordService();
        BetRecordService betService = new BetRecordService();
        DbService dbService = new DbService();
        UserInfoService userService = new UserInfoService();
        AccountService accService = new AccountService();

        /// <summary>
        /// 根据官方开奖结果处理用户所有下注信息
        /// </summary>
        /// <param name="?"></param>
        public void ProcessBet(LotteryResult lotteryRes)
        {
            List<BetRecord> Recs = betService.LoadEntities(s => s.LotteryType.Equals(lotteryRes.LotteryType) && s.Expect.Equals(lotteryRes.Expect) && s.LotteryState.Equals("未开奖")).ToList();
            foreach (BetRecord item in Recs)
            {
                //先获取所有该类型该期号下的单注下注信息
                List<BetChildRecord> ChildRecs = betCService.LoadEntities(s => s.BetRecordId.Value==item.Id).ToList();
                decimal? WinMoney = 0;
                foreach (BetChildRecord Childitem in ChildRecs)
                {

                    bool isWin = false;
                    int opencode = int.Parse(lotteryRes.Opencode.Split(',')[Childitem.BetBallNo.Value - 1]);
                    //计算开奖结果
                    switch (Childitem.BetType)
                    {
                        case "单双":
                            isWin = isWinForSingleDouble(Childitem.BetInfo, opencode);
                            break;
                        case "大小":
                            isWin = isWinForBigSmall(Childitem.LotteryType, Childitem.BetInfo, opencode);
                            break;
                        case "数字":
                            isWin = isWinForNo(Childitem.BetInfo, opencode);
                            break;
                        default:
                            break;
                    }
                    if (isWin)
                    { //如果中奖则修改单注状态并累加父注输赢金额
                        Childitem.WinState = "中奖";
                       
                        WinMoney += Childitem.BetMoney * decimal.Parse(Childitem.Odds.ToString());
                        //用户余额增加
                        UserInfo user = userService.LoadEntities(s => s.Name.Equals(item.BetUser)).FirstOrDefault();
                        if (user != null)
                        {
                            Account acc = accService.LoadEntities(s => s.UserId == user.id).FirstOrDefault();
                            if (acc != null)
                            {
                                acc.Balance += Childitem.BetMoney.Value * decimal.Parse(Childitem.Odds.ToString());
                                accService.UpdateEntityNoCommit(acc);
                            }
                        }
                    }
                    else
                        Childitem.WinState = "未中奖";
                    betCService.UpdateEntityNoCommit(Childitem);
                }
                item.TotalWinOrLose = WinMoney;
                item.LotteryState = "已开奖";
                betService.UpdateEntityNoCommit(item);
                dbService.Savechange();
            }
                


        }
        /// <summary>
        /// 单双判断输赢
        /// </summary>
        /// <param name="opencode"></param>
        /// <returns></returns>
        private bool isWinForSingleDouble(string BetInfo,int opencode)
        {
            if(BetInfo.Equals("双"))
                return opencode % 2 == 0;
            else
                return opencode % 2 != 0;
        }
        /// <summary>
        /// 大小判断输赢
        /// </summary>
        /// <param name="BetInfo"></param>
        /// <param name="opencode"></param>
        /// <returns></returns>
        private bool isWinForBigSmall(string LotteryType,string BetInfo, int opencode)
        {
            int BaseInt=0;
            switch (LotteryType)
            {
                case "广东快乐十分":
                    BaseInt = 10;
                    break;
                case "重庆时时彩":
                    BaseInt = 4;
                    break;
                case "广东11选5":
                    BaseInt = 5;
                    break;
                default:
                    break;
            }
            if (BetInfo.Equals("大"))
                return opencode > BaseInt;
            else
                return opencode <= BaseInt;

        }
        /// <summary>
        /// 数字是否中奖
        /// </summary>
        /// <param name="BetInfo"></param>
        /// <param name="opencode"></param>
        /// <returns></returns>
        private bool isWinForNo(string BetInfo, int opencode)
        {
            return int.Parse(BetInfo) == opencode;
        }

    }
}
