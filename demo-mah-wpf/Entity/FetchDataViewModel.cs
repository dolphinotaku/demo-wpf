using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace demo_mah_wpf.Entity
{
    public class FetchDataViewModel : BaseViewModel
    {
        protected readonly DispatcherTimer timer = new DispatcherTimer();
        protected int defaultRefreshDataInSecond = 5;
        protected int currentSecond = -1;
        protected string displayRefreshCountdownText
        {
            get
            {
                return string.Format("Refresh in {} second", currentSecond);
            }
        }

        public FetchDataViewModel()
        {
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += TimerTick;
            timer.Start();
        }
        private async void TimerTick(object sender, EventArgs args)
        {
            this.currentSecond += 1;
            this.CustomTimerTick(sender, args);
        }
        public async virtual void CustomTimerTick(object sender, EventArgs args)
        {
            await System.Threading.Tasks.Task.Delay(0);
        }
    }
}
