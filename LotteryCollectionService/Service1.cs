using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace LotteryCollectionService
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            //System.Threading.Thread.Sleep(1000 * 20);
            //LetteryService.GetLetteryInfo();
            //LetteryService.Start();
            Timer timerDelay = new System.Timers.Timer(1000 * 60 * 1);
            timerDelay.Elapsed += new System.Timers.ElapsedEventHandler(timerDelay_Elapsed);
            timerDelay.AutoReset = true;
            timerDelay.Start();
        }
        public void timerDelay_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            LetteryService.GetLetteryInfo();
        }
        protected override void OnStop()
        {
        }
    }
}
