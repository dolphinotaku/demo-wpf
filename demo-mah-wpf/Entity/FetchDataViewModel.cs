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
        protected readonly DispatcherTimer refreshDataTimer = new DispatcherTimer();
        protected readonly DispatcherTimer refreshPageTimer = new DispatcherTimer();

        protected readonly int defaultTimeInterval = 1;
        protected int currentSecond = -1;

        private int _defaultRefreshDataInEverySecond; // define how long will refresh data
        protected int RefaultRefreshDataInEverySecond { get { return this._defaultRefreshDataInEverySecond; } }
        protected int RefreshDataCountdownInSecond // get countdown number of how long take to refresh data
        {
            get
            {
                if (this.currentSecond > 0)
                {
                    return this._defaultRefreshDataInEverySecond - (currentSecond % this._defaultRefreshDataInEverySecond);
                }
                else
                {
                    return this._defaultRefreshDataInEverySecond;
                }
            }
        }

        protected int _defaultRefreshPageInEverySecond; // define how long will refresh display
        protected int DefaultRefreshPageInEverySecond { get { return this._defaultRefreshPageInEverySecond; } }
        protected int RefreshPageCountdownInSecond // get countdown number of how long take to refresh display
        {
            get
            {
                if (this.currentSecond > 0)
                {
                    return this._defaultRefreshPageInEverySecond - (currentSecond % this._defaultRefreshPageInEverySecond);
                }
                else
                {
                    return this._defaultRefreshPageInEverySecond;
                }
            }
        }

        protected string displayRefreshCountdownText
        {
            get
            {
                return string.Format("Refresh in {} second", currentSecond);
            }
        }

        public FetchDataViewModel()
        {
            this._defaultRefreshDataInEverySecond = 5; // define how long will refresh data
            this._defaultRefreshPageInEverySecond = 10; // define how long will refresh display
            // to do, read config file to get the above value

            refreshDataTimer.Interval = TimeSpan.FromSeconds(this.defaultTimeInterval);
            refreshDataTimer.Tick += DataTimerTick;
            refreshDataTimer.Start();

            refreshPageTimer.Interval = TimeSpan.FromSeconds(10);
            refreshPageTimer.Tick += DataTimerTick;
            refreshPageTimer.Start();
        }
        private async void DataTimerTick(object sender, EventArgs args)
        {
            this.currentSecond += 1;
            this.CustomDataTimerTick(sender, args);
        }
        public async virtual void CustomDataTimerTick(object sender, EventArgs args)
        {
            await Task.Delay(0);
        }
        private async void PageTimerTick(object sender, EventArgs args)
        {
            //this.currentSecond += 1;
            this.CustomPageTimerTick(sender, args);
        }
        public async virtual void CustomPageTimerTick(object sender, EventArgs args)
        {
            await Task.Delay(0);
        }
    }
}
