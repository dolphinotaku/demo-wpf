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
    public class WalkInBookingViewModel : FetchDataViewModel
    {
        protected WalkInBooking firstDisplayBookingNode;
        protected WalkInBooking lastDisplayBookingNode;
        protected VoiceService voiceService;
        protected AutoMapperService automapperService;

        private WalkInBookingPagination _TaskCollection1;
        public WalkInBookingPagination WalkInBookingPagination
        {
            get { return _TaskCollection1; }
            set
            {
                _TaskCollection1 = value;
                this.SetProperty(ref _TaskCollection1, value, () => WalkInBookingPagination);
            }
        }

        public IEnumerable<WalkInBooking> taskQueryList;


        public WalkInBookingViewModel(IAutoMapperService automapperService) : base()
        {
            this.taskQueryList = new List<WalkInBooking>();
            this.WalkInBookingPagination = new WalkInBookingPagination();

            //this.voiceService = voiceService.GetInstance();
            this.automapperService = automapperService.GetInstance();

            this.RefaultRefreshDataInEverySecond = 5;

            //this.WalkInBookingPagination.Add(new WalkInBooking("Laundry", "Do my Laundry", 2, TaskType.Home));
            //this.WalkInBookingPagination.Add(new WalkInBooking("Email", "Email clients", 1, TaskType.Work));
            //this.WalkInBookingPagination.Add(new WalkInBooking("Clean", "Clean my office", 3, TaskType.Work));
            //this.WalkInBookingPagination.Add(new WalkInBooking("Dinner", "Get ready for family reunion", 1, TaskType.Home));
            //this.WalkInBookingPagination.Add(new WalkInBooking("Proposals", "Review new budget proposals", 2, TaskType.Work));

            // create two column
            WalkInBookingPagination _bookingPage = new WalkInBookingPagination();

            // add empty 6 rows
            IEnumerable<WalkInBooking> _taskDataResult = (IEnumerable<WalkInBooking>)this.CreateEmptyData(6);
            List<WalkInBooking> _taskList = _taskDataResult.ToList();
            _bookingPage.AddBookingRange(_taskList);
            this.WalkInBookingPagination = _bookingPage;

            this.taskQueryList = _taskList;
        }

        public override async void CustomDataTimerTick(object sender, EventArgs args)
        {
            //if (this.currentSecond % this.RefaultRefreshDataInEverySecond != 0) return;

            //this.WalkInBookingPagination.Clear();
            IEnumerable<WalkInBooking> _taskDataResult = (IEnumerable<WalkInBooking>)await this.GetAllData();
            List<WalkInBooking> _bookingList = _taskDataResult.ToList();
            if (_bookingList != null && _bookingList.Count > 0)
            {
                this.WalkInBookingPagination.ClearBookingList();
                this.WalkInBookingPagination.AddBookingRange(_bookingList);
                //for (int i = 0; i < _bookingList.Count; i++)
                //{
                //this.WalkInBookingPagination.Add(new WalkInBooking(string.Format("Development {0}", i), string.Format("Write a WPF program {0}", DateTime.Now.ToString()), 2, TaskType.Home));
                //var _task = _bookingList[i];
                //this.WalkInBookingPagination[i].TaskName = _task.TaskName;
                //this.WalkInBookingPagination[i].Description = _task.Description;
                //this.WalkInBookingPagination[i].Priority = _task.Priority;
                //this.WalkInBookingPagination[i].TaskType = _task.TaskType;
                //}
            }
            await Task.Delay(0);
            return;
        }
        public override void CustomPageTimerTick(object sender, EventArgs args)
        {
            this.WalkInBookingPagination.RefreshDisplayColumn();
        }
        private List<WalkInBooking> dummyData30List3 = new List<WalkInBooking>()
        {
            new WalkInBooking("W001", "S1", 2, TaskType.Home),
            new WalkInBooking("W002", "S2", 2, TaskType.Home),
            new WalkInBooking("W003", "S3", 2, TaskType.Home),
            new WalkInBooking("W004", "S4", 2, TaskType.Home),
            new WalkInBooking("W005", "S5", 2, TaskType.Home),
            new WalkInBooking("W006", "S6", 2, TaskType.Home),
            new WalkInBooking("W007", "S7", 2, TaskType.Home),
            new WalkInBooking("W008", "S8", 2, TaskType.Home),
            new WalkInBooking("W009", "S9", 2, TaskType.Home),
            new WalkInBooking("W010", "S10", 2, TaskType.Home),
            new WalkInBooking("W011", "S11", 2, TaskType.Home),
            new WalkInBooking("W012", "S12", 2, TaskType.Home),
            new WalkInBooking("W013", "S13", 2, TaskType.Home),
            new WalkInBooking("W014", "S14", 2, TaskType.Home),
            new WalkInBooking("W015", "S15", 2, TaskType.Home),
            new WalkInBooking("W016", "S16", 2, TaskType.Home),
            new WalkInBooking("W017", "S17", 2, TaskType.Home),
            new WalkInBooking("W018", "S18", 2, TaskType.Home),
            new WalkInBooking("W019", "S19", 2, TaskType.Home),
            new WalkInBooking("W020", "S20", 2, TaskType.Home),
            new WalkInBooking("W021", "S21", 2, TaskType.Home),
            new WalkInBooking("W022", "S22", 2, TaskType.Home),
            new WalkInBooking("W023", "S23", 2, TaskType.Home),
            new WalkInBooking("W024", "S24", 2, TaskType.Home),
            new WalkInBooking("W025", "S25", 2, TaskType.Home),
            new WalkInBooking("W026", "S26", 2, TaskType.Home),
        };

        private List<WalkInBooking> dummyData20List3 = new List<WalkInBooking>()
        {
            new WalkInBooking("W041", "S1", 2, TaskType.Home),
            new WalkInBooking("W042", "S2", 2, TaskType.Home),
            new WalkInBooking("W043", "S3", 2, TaskType.Home),
            new WalkInBooking("W044", "S4", 2, TaskType.Home),
            new WalkInBooking("W045", "S5", 2, TaskType.Home),
            new WalkInBooking("W046", "S6", 2, TaskType.Home),
            new WalkInBooking("W047", "S7", 2, TaskType.Home),
            new WalkInBooking("W048", "S8", 2, TaskType.Home),
            new WalkInBooking("W049", "S9", 2, TaskType.Home),
            new WalkInBooking("W050", "S10", 2, TaskType.Home),
            new WalkInBooking("W051", "S11", 2, TaskType.Home),
            new WalkInBooking("W052", "S12", 2, TaskType.Home),
            new WalkInBooking("W053", "S13", 2, TaskType.Home),
            new WalkInBooking("W054", "S14", 2, TaskType.Home),
            new WalkInBooking("W055", "S15", 2, TaskType.Home),
            new WalkInBooking("W056", "S16", 2, TaskType.Home),
            new WalkInBooking("W057", "S17", 2, TaskType.Home),
            new WalkInBooking("W058", "S18", 2, TaskType.Home),
            new WalkInBooking("W059", "S19", 2, TaskType.Home),
            new WalkInBooking("W060", "S20", 2, TaskType.Home),
        };

        private List<WalkInBooking> dummyData10List3 = new List<WalkInBooking>()
        {
            new WalkInBooking("W061", "S1", 2, TaskType.Home),
            new WalkInBooking("W062", "S2", 2, TaskType.Home),
            new WalkInBooking("W063", "S3", 2, TaskType.Home),
            new WalkInBooking("W064", "S4", 2, TaskType.Home),
            new WalkInBooking("W065", "S5", 2, TaskType.Home),
        };

        public Task<List<WalkInBooking>> GetAllData()
        {
            List<WalkInBooking> _taskList = new List<WalkInBooking>();
            // test scenarios
            DateTime dateTime = DateTime.Now;
            if (dateTime.Second == 0)
            {
                _taskList = this.dummyData20List3;
            }
            else if (dateTime.Second == 20)
            {
                _taskList = this.dummyData10List3;
            }
            else if (dateTime.Second == 30)
            {
                _taskList = this.dummyData30List3;
            }
            return Task.FromResult<List<WalkInBooking>>(_taskList);

            Char[] ticketType = new Char[] { 'W' };
            Char[] roomType = new Char[] { ' ', 'S' };
            for (int i = 1; i <= 12; i++)
            {
                var _ticketNum = this.GetRandomInt(1, 99).ToString("D3");
                var _roomNum = this.GetRandomInt(1, 30).ToString();
                _taskList.Add(new WalkInBooking(
                    string.Format("{0}{1}",
                        ticketType[this.GetRandomInt(0, ticketType.Length)], _ticketNum),
                    string.Format("{0}{1}",
                        roomType[this.GetRandomInt(0, roomType.Length)], _roomNum),
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
