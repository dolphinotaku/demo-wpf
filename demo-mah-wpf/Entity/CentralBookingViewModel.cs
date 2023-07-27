using demo_mah_wpf.Entity;
using demo_mah_wpf.Master;
using demo_mah_wpf.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace demo_mah_wpf
{
    public class CentralBookingViewModel : FetchDataViewModel
    {
        protected CentralBooking firstDisplayBookingNode;
        protected CentralBooking lastDisplayBookingNode;
        protected IVoiceService _voiceService;
        protected IAutoMapperService _autoMapperService;

        private CentralBookingPagination _TaskCollection1;
        public CentralBookingPagination CentralBookingPagination
        {
            get { return _TaskCollection1; }
            set
            {
                _TaskCollection1 = value;
                SetProperty(ref _TaskCollection1, value, () => CentralBookingPagination);
            }
        }

        public IEnumerable<CentralBooking> taskQueryList;


        public CentralBookingViewModel(IVoiceService voiceService, IAutoMapperService automapperService) : base()
        {
            taskQueryList = new List<CentralBooking>();
            CentralBookingPagination = new CentralBookingPagination();

            _voiceService = voiceService;
            _autoMapperService = automapperService;

            RefaultRefreshDataInEverySecond = 1;

            // create two column
            CentralBookingPagination _bookingPage = new CentralBookingPagination();

            // add empty 6 rows
            IEnumerable<CentralBooking> _taskDataResult = (IEnumerable<CentralBooking>)CreateEmptyData(6);
            List<CentralBooking> _taskList = _taskDataResult.ToList();
            _bookingPage.AddBookingRange(_taskList);
            CentralBookingPagination = _bookingPage;

            taskQueryList = _taskList;
        }

        public override async void CustomDataTimerTick(object sender, EventArgs args)
        {
            //CentralBookingPagination.Clear();
            IEnumerable<CentralBooking> _taskDataResult = (IEnumerable<CentralBooking>)await GetAllData();
            List<CentralBooking> _bookingList = _taskDataResult.ToList();
            if (_bookingList != null && _bookingList.Count > 0)
            {
                CentralBookingPagination.ClearBookingList();
                CentralBookingPagination.AddBookingRange(_bookingList);
                var voiceList = _autoMapperService.Instance.Map<List<CentralBooking>, List<VoiceAnnouncement>>(_bookingList);
                _voiceService.AddRange(voiceList);
            }
        }
        public override void CustomPageTimerTick(object sender, EventArgs args)
        {
            CentralBookingPagination.RefreshDisplayColumn();
        }

        private List<CentralBooking> dummyData30List3 = new List<CentralBooking>()
        {
            new CentralBooking("C001", "1", 2, TaskType.Home),
            new CentralBooking("C002", "2", 2, TaskType.Home),
            new CentralBooking("C003", "3", 2, TaskType.Home),
            new CentralBooking("C004", "4", 2, TaskType.Home),
            new CentralBooking("C005", "5", 2, TaskType.Home),
            new CentralBooking("C006", "6", 2, TaskType.Home),
            new CentralBooking("C007", "7", 2, TaskType.Home),
            new CentralBooking("C008", "8", 2, TaskType.Home),
            new CentralBooking("C009", "9", 2, TaskType.Home),
            new CentralBooking("C010", "10", 2, TaskType.Home)
        };

        private List<CentralBooking> dummyData20List3 = new List<CentralBooking>()
        {
            new CentralBooking("C041", "1", 2, TaskType.Home),
            new CentralBooking("C042", "2", 2, TaskType.Home),
            new CentralBooking("C043", "3", 2, TaskType.Home),
            new CentralBooking("C044", "4", 2, TaskType.Home),
            new CentralBooking("C045", "5", 2, TaskType.Home),
            new CentralBooking("C046", "6", 2, TaskType.Home),
            new CentralBooking("C047", "7", 2, TaskType.Home)
        };

        private List<CentralBooking> dummyData10List3 = new List<CentralBooking>()
        {
            new CentralBooking("C061", "1", 2, TaskType.Home),
            new CentralBooking("C062", "2", 2, TaskType.Home),
            new CentralBooking("C063", "3", 2, TaskType.Home),
            new CentralBooking("C064", "4", 2, TaskType.Home),
            new CentralBooking("C065", "5", 2, TaskType.Home)
        };

        public Task<List<CentralBooking>> GetAllData()
        {
            List<CentralBooking> _taskList = new List<CentralBooking>();
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
            return Task.FromResult<List<CentralBooking>>(_taskList);

            Char[] ticketType = new Char[] { 'S', 'C' };
            Char[] roomType = new Char[] { ' ', 'S' };
            for (int i = 1; i <= 12; i++)
            {
                var _ticketNum = GetRandomInt(1, 99).ToString("D3");
                var _roomNum = GetRandomInt(1, 30).ToString();
                _taskList.Add(new CentralBooking(
                    string.Format("{0}{1}",
                        ticketType[GetRandomInt(0, ticketType.Length)], _ticketNum),
                    string.Format("{0}{1}",
                        roomType[GetRandomInt(0, roomType.Length)], _roomNum),
                    2, TaskType.Home));
            }

            return Task.FromResult<List<CentralBooking>>(_taskList);
        }


        public List<CentralBooking> CreateEmptyData(int rowsCount = 12)
        {
            List<CentralBooking> _taskList = new List<CentralBooking>();
            for (int i = 1; i <= 12; i++)
            {
                _taskList.Add(new CentralBooking(string.Format("Empty {0}", i), string.Format("Description {0}", DateTime.Now.ToString()), 2, TaskType.Home));
            }

            return _taskList;
        }
    }
}
