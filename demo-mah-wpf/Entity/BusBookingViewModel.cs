using demo_mah_wpf.Entity;
using demo_mah_wpf.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace demo_mah_wpf
{
    public class BusBookingViewModel : FetchDataViewModel
    {
        protected BusBooking firstDisplayBookingNode;
        protected BusBooking lastDisplayBookingNode;
        protected VoiceService voiceService;
        protected AutoMapperService automapperService;

        private BusBookingPagination _TaskCollection1;
        public BusBookingPagination BusBookingPagination
        {
            get { return _TaskCollection1; }
            set
            {
                _TaskCollection1 = value;
                this.SetProperty(ref _TaskCollection1, value, () => BusBookingPagination);
            }
        }

        public IEnumerable<BusBooking> taskQueryList;


        public BusBookingViewModel(IAutoMapperService automapperService) : base()
        {
            this.taskQueryList = new List<BusBooking>();
            this.BusBookingPagination = new BusBookingPagination();

            //this.voiceService = voiceService.GetInstance();
            this.automapperService = automapperService.GetInstance();

            this.RefaultRefreshDataInEverySecond = 5;

            //this.BusBookingPagination.Add(new BusBooking("Laundry", "Do my Laundry", 2, TaskType.Home));
            //this.BusBookingPagination.Add(new BusBooking("Email", "Email clients", 1, TaskType.Work));
            //this.BusBookingPagination.Add(new BusBooking("Clean", "Clean my office", 3, TaskType.Work));
            //this.BusBookingPagination.Add(new BusBooking("Dinner", "Get ready for family reunion", 1, TaskType.Home));
            //this.BusBookingPagination.Add(new BusBooking("Proposals", "Review new budget proposals", 2, TaskType.Work));

            // create two column
            BusBookingPagination _bookingPage = new BusBookingPagination();

            // add empty 6 rows
            IEnumerable<BusBooking> _taskDataResult = (IEnumerable<BusBooking>)this.CreateEmptyData(6);
            List<BusBooking> _taskList = _taskDataResult.ToList();
            _bookingPage.AddBookingRange(_taskList);
            this.BusBookingPagination = _bookingPage;

            this.taskQueryList = _taskList;
        }

        public override async void CustomDataTimerTick(object sender, EventArgs args)
        {
            if (this.currentSecond % this.RefaultRefreshDataInEverySecond != 0) return;

            //this.BusBookingPagination.Clear();
            IEnumerable<BusBooking> _taskDataResult = (IEnumerable<BusBooking>)await this.GetAllData();
            List<BusBooking> _bookingList = _taskDataResult.ToList();
            if (_bookingList != null && _bookingList.Count > 0)
            {
                this.BusBookingPagination.ClearBookingList();
                this.BusBookingPagination.AddBookingRange(_bookingList);
                //for (int i = 0; i < _bookingList.Count; i++)
                //{
                //this.BusBookingPagination.Add(new BusBooking(string.Format("Development {0}", i), string.Format("Write a WPF program {0}", DateTime.Now.ToString()), 2, TaskType.Home));
                //var _task = _bookingList[i];
                //this.BusBookingPagination[i].TaskName = _task.TaskName;
                //this.BusBookingPagination[i].Description = _task.Description;
                //this.BusBookingPagination[i].Priority = _task.Priority;
                //this.BusBookingPagination[i].TaskType = _task.TaskType;
                //}
            }
            await Task.Delay(0);
            return;
        }
        public override void CustomPageTimerTick(object sender, EventArgs args)
        {
            this.BusBookingPagination.RefreshDisplayColumn();
        }

        public Task<List<BusBooking>> GetAllData()
        {
            List<BusBooking> _taskList = new List<BusBooking>();
            Char[] ticketType = new Char[] { 'W' };
            Char[] roomType = new Char[] { 'P' };
            Char[] classType = new Char[] { 'A', 'B', 'C', 'D', 'E' };
            int[] minutes = new int[] { 0, 30 };
            string[] shttleBus = new string[]
            {
                "ABCDEFGHIJ0123456789ABCDEFGHIJ0123456789ABCDEFGHIJ0123456789ABCDEFGHIJ0123456789",
"Bonham Road Government Primary School 般咸道官立小學",
"Kau Yan School 救恩學校",
"King's College Old Boys' Association Primary School 英皇書院同學會小學",
"King's College Old Boys' Association Primary School No.2 英皇書院同學會小學第二校",
"St. Charles School 聖嘉祿學校",
"St. Clare's Primary School 聖嘉勒小學",
"St. Louis School (Primary Section) 聖類斯中學(小學部)",
"St. Stephen's Girls' Primary School 聖士提反女子中學附屬小學",
"St. Paul's Primary School 聖保祿天主教小學"
            };

            for (int i = 1; i <= 10; i++)
            {
                var _ticketNum = this.GetRandomInt(1, 99).ToString("D3");
                var _roomNum = this.GetRandomInt(1, 30).ToString();
                TimeSpan timeSpan = new TimeSpan(this.GetRandomInt(1, 24), minutes[this.GetRandomInt(0, 1)], 0);
                var _datetime = new DateTime();
                _datetime = _datetime + timeSpan;

                _taskList.Add(new BusBooking(
                    _datetime,
                    string.Format("{0}",
                        shttleBus[this.GetRandomInt(0, shttleBus.Length)]),
                    string.Format("P.{0}{1}",
                        this.GetRandomInt(1, 6), classType[this.GetRandomInt(0, classType.Length)]),
                    2, TaskType.Home));
            }

            return Task.FromResult<List<BusBooking>>(_taskList);
        }


        public List<BusBooking> CreateEmptyData(int rowsCount = 12)
        {
            List<BusBooking> _taskList = new List<BusBooking>();
            for (int i = 1; i <= 12; i++)
            {
                _taskList.Add(new BusBooking(new DateTime(), string.Format("Empty {0}", i), string.Format("Description {0}", DateTime.Now.ToString()), 2, TaskType.Home));
            }

            return _taskList;
        }
    }
}
