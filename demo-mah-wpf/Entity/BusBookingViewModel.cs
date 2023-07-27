using demo_mah_wpf.Entity;
using demo_mah_wpf.Master;
using demo_mah_wpf.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace demo_mah_wpf
{
    public class BusBookingViewModel : FetchDataViewModel
    {
        protected BusBooking firstDisplayBookingNode;
        protected BusBooking lastDisplayBookingNode;
        protected IVoiceService _voiceService;
        protected IAutoMapperService _automapperService;

        private BusBookingPagination _TaskCollection1;
        public BusBookingPagination BusBookingPagination
        {
            get { return _TaskCollection1; }
            set
            {
                _TaskCollection1 = value;
                SetProperty(ref _TaskCollection1, value, () => BusBookingPagination);
            }
        }

        public IEnumerable<BusBooking> _taskQueryList;


        public BusBookingViewModel(IVoiceService voiceService, IAutoMapperService automapperService) : base()
        {
            _taskQueryList = new List<BusBooking>();
            BusBookingPagination = new BusBookingPagination();

            _voiceService = voiceService;
            _automapperService = automapperService;

            RefaultRefreshDataInEverySecond = 5;
            // create two column
            BusBookingPagination _bookingPage = new BusBookingPagination();

            // add empty 6 rows
            IEnumerable<BusBooking> _taskDataResult = (IEnumerable<BusBooking>)CreateEmptyData(6);
            List<BusBooking> _taskList = _taskDataResult.ToList();
            _bookingPage.AddBookingRange(_taskList);
            BusBookingPagination = _bookingPage;

            _taskQueryList = _taskList;
        }

        public override async void CustomDataTimerTick(object sender, EventArgs args)
        {
            if (currentSecond % RefaultRefreshDataInEverySecond != 0) return;

            //BusBookingPagination.Clear();
            IEnumerable<BusBooking> _taskDataResult = (IEnumerable<BusBooking>)await GetAllData();
            List<BusBooking> _bookingList = _taskDataResult.ToList();
            if (_bookingList != null && _bookingList.Count > 0)
            {
                BusBookingPagination.ClearBookingList();
                BusBookingPagination.AddBookingRange(_bookingList);
                var voiceList = _automapperService.Instance.Map<List<BusBooking>, List<VoiceAnnouncement>>(_bookingList);
                _voiceService.AddRange(voiceList);
            }
            return;
        }
        public override void CustomPageTimerTick(object sender, EventArgs args)
        {
            BusBookingPagination.RefreshDisplayColumn();
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
                var _ticketNum = GetRandomInt(1, 99).ToString("D3");
                var _roomNum = GetRandomInt(1, 30).ToString();
                TimeSpan timeSpan = new TimeSpan(GetRandomInt(1, 24), minutes[GetRandomInt(0, 1)], 0);
                var _datetime = new DateTime();
                _datetime = _datetime + timeSpan;

                _taskList.Add(new BusBooking(
                    _datetime,
                    string.Format("{0}",
                        shttleBus[GetRandomInt(0, shttleBus.Length)]),
                    string.Format("P.{0}{1}",
                        GetRandomInt(1, 6), classType[GetRandomInt(0, classType.Length)]),
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
