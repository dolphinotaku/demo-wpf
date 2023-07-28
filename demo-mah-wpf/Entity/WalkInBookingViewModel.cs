using demo_mah_wpf.Entity;
using demo_mah_wpf.Master;
using demo_mah_wpf.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace demo_mah_wpf
{
    public class WalkInBookingViewModel : FetchDataViewModel
    {
        protected WalkInBooking firstDisplayBookingNode;
        protected WalkInBooking lastDisplayBookingNode;
        protected IVoiceService _voiceService;
        protected IAutoMapperService _autoMapperService;

        private WalkInBookingPagination _TaskCollection1;
        public WalkInBookingPagination WalkInBookingPagination
        {
            get { return _TaskCollection1; }
            set
            {
                _TaskCollection1 = value;
                SetProperty(ref _TaskCollection1, value, () => WalkInBookingPagination);
            }
        }

        public IEnumerable<WalkInBooking> taskQueryList;

        public WalkInBookingViewModel(IVoiceService voiceService, IAutoMapperService automapperService) : base()
        {
            taskQueryList = new List<WalkInBooking>();
            WalkInBookingPagination = new WalkInBookingPagination();

            _voiceService = voiceService;
            _autoMapperService = automapperService;

            RefaultRefreshDataInEverySecond = 5;

            // create two column
            WalkInBookingPagination _bookingPage = new WalkInBookingPagination();

            // add empty 6 rows
            IEnumerable<WalkInBooking> _taskDataResult = (IEnumerable<WalkInBooking>)CreateEmptyData(6);
            List<WalkInBooking> _taskList = _taskDataResult.ToList();
            _bookingPage.AddBookingRange(_taskList);
            WalkInBookingPagination = _bookingPage;

            taskQueryList = _taskList;
        }

        public override async void CustomDataTimerTick(object sender, EventArgs args)
        {
            //if (currentSecond % RefaultRefreshDataInEverySecond != 0) return;

            //WalkInBookingPagination.Clear();
            IEnumerable<WalkInBooking> _taskDataResult = (IEnumerable<WalkInBooking>)await GetAllData();
            List<WalkInBooking> _bookingList = _taskDataResult.ToList();
            if (_bookingList != null && _bookingList.Count > 0)
            {
                WalkInBookingPagination.ClearBookingList();
                WalkInBookingPagination.AddBookingRange(_bookingList);
                var voiceList = _autoMapperService.Instance.Map<List<WalkInBooking>, List<VoiceAnnouncement>>(_bookingList);
                _voiceService.AddRange(voiceList);

            }
        }
        public override void CustomPageTimerTick(object sender, EventArgs args)
        {
            WalkInBookingPagination.RefreshDisplayColumn();
        }
        private List<WalkInBooking> dummyData30List3 = new List<WalkInBooking>()
        {
            new WalkInBooking("W001", "S1", 2, TaskType.Home),
            new WalkInBooking("W002", "S2", 2, TaskType.Home),
            //new WalkInBooking("W003", "S3", 2, TaskType.Home),
            //new WalkInBooking("W004", "S4", 2, TaskType.Home),
            //new WalkInBooking("W005", "S5", 2, TaskType.Home),
            //new WalkInBooking("W006", "S6", 2, TaskType.Home),
            //new WalkInBooking("W007", "S7", 2, TaskType.Home),
            //new WalkInBooking("W008", "S8", 2, TaskType.Home),
            //new WalkInBooking("W009", "S9", 2, TaskType.Home),
            //new WalkInBooking("W010", "S10", 2, TaskType.Home)
        };

        private List<WalkInBooking> dummyData20List3 = new List<WalkInBooking>()
        {
            new WalkInBooking("W001", "S1", 2, TaskType.Home),
            new WalkInBooking("W002", "S2", 2, TaskType.Home),
            new WalkInBooking("W041", "S11", 2, TaskType.Home),
        };

        private List<WalkInBooking> dummyData10List3 = new List<WalkInBooking>()
        {
            new WalkInBooking("W001", "S1", 2, TaskType.Home),
            new WalkInBooking("W002", "S2", 2, TaskType.Home),
            new WalkInBooking("W041", "S11", 2, TaskType.Home),
            new WalkInBooking("W061", "S21", 2, TaskType.Home),
        };

        public Task<List<WalkInBooking>> GetAllData()
        {
            List<WalkInBooking> _taskList = new List<WalkInBooking>();
            // test scenarios
            DateTime dateTime = DateTime.Now;
            if (dateTime.Second == 0)
            {
                _taskList = dummyData20List3;
            }
            else if (dateTime.Second == 20)
            {
                _taskList = dummyData10List3;
            }
            else if (dateTime.Second == 30)
            {
                _taskList = dummyData30List3;
            }
            return Task.FromResult<List<WalkInBooking>>(_taskList);

            Char[] ticketType = new Char[] { 'W' };
            Char[] roomType = new Char[] { ' ', 'S' };
            for (int i = 1; i <= 12; i++)
            {
                var _ticketNum = GetRandomInt(1, 99).ToString("D3");
                var _roomNum = GetRandomInt(1, 30).ToString();
                _taskList.Add(new WalkInBooking(
                    string.Format("{0}{1}",
                        ticketType[GetRandomInt(0, ticketType.Length)], _ticketNum),
                    string.Format("{0}{1}",
                        roomType[GetRandomInt(0, roomType.Length)], _roomNum),
                    2, TaskType.Home));
            }

            return Task.FromResult<List<WalkInBooking>>(_taskList);
        }


        public List<WalkInBooking> CreateEmptyData(int rowsCount = 12)
        {
            List<WalkInBooking> _taskList = new List<WalkInBooking>();
            for (int i = 1; i <= 12; i++)
            {
                _taskList.Add(new WalkInBooking(string.Format("Empty {0}", i), string.Format("Description {0}", DateTime.Now.ToString()), 2, TaskType.Home));
            }

            return _taskList;
        }
    }
}
