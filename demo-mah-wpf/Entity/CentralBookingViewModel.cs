using demo_mah_wpf.Entity;
using demo_mah_wpf.Master;
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
    public class CentralBookingViewModel : FetchDataViewModel
    {
        protected CentralBooking firstDisplayBookingNode;
        protected CentralBooking lastDisplayBookingNode;
        protected VoiceService voiceService;
        protected AutoMapperService automapperService;

        private CentralBookingPagination _TaskCollection1;
        public CentralBookingPagination CentralBookingPagination
        {
            get { return _TaskCollection1; }
            set
            {
                _TaskCollection1 = value;
                this.SetProperty(ref _TaskCollection1, value, () => CentralBookingPagination);
            }
        }

        public IEnumerable<CentralBooking> taskQueryList;


        public CentralBookingViewModel(IVoiceService voiceService, IAutoMapperService automapperService) : base()
        {
            this.taskQueryList = new List<CentralBooking>();
            this.CentralBookingPagination = new CentralBookingPagination();

            this.voiceService = voiceService.GetInstance();
            this.automapperService = automapperService.GetInstance();

            this.RefaultRefreshDataInEverySecond = 1;

            //this.CentralBookingPagination.Add(new CentralBooking("Laundry", "Do my Laundry", 2, TaskType.Home));
            //this.CentralBookingPagination.Add(new CentralBooking("Email", "Email clients", 1, TaskType.Work));
            //this.CentralBookingPagination.Add(new CentralBooking("Clean", "Clean my office", 3, TaskType.Work));
            //this.CentralBookingPagination.Add(new CentralBooking("Dinner", "Get ready for family reunion", 1, TaskType.Home));
            //this.CentralBookingPagination.Add(new CentralBooking("Proposals", "Review new budget proposals", 2, TaskType.Work));

            // create two column
            CentralBookingPagination _bookingPage = new CentralBookingPagination();

            // add empty 6 rows
            IEnumerable<CentralBooking> _taskDataResult = (IEnumerable<CentralBooking>)this.CreateEmptyData(6);
            List<CentralBooking> _taskList = _taskDataResult.ToList();
            _bookingPage.AddBookingRange(_taskList);
            this.CentralBookingPagination = _bookingPage;

            this.taskQueryList = _taskList;
        }

        public override async void CustomDataTimerTick(object sender, EventArgs args)
        {
            // if the voice is speaking, stop page change
            if (this.voiceService.GetInstance().IsCurrentlySpeaking) return;
            //if (this.currentSecond % this.RefaultRefreshDataInEverySecond != 0) return;

            //this.CentralBookingPagination.Clear();
            IEnumerable<CentralBooking> _taskDataResult = (IEnumerable<CentralBooking>)await this.GetAllData();
            List<CentralBooking> _bookingList = _taskDataResult.ToList();
            if (_bookingList != null && _bookingList.Count > 0)
            {
                this.CentralBookingPagination.ClearBookingList();
                this.CentralBookingPagination.AddBookingRange(_bookingList);

                List<VoiceAnnouncement> voiceList = new List<VoiceAnnouncement>();
                var mapper = this.automapperService.InitializeAutomapper();
                voiceList = mapper.Map<List<CentralBooking>, List<VoiceAnnouncement>>(_bookingList);

                this.voiceService.AddRange(voiceList);
                //voiceList = this
                //this.voiceService.AddRange();

                //for (int i = 0; i < _bookingList.Count; i++)
                //{
                //this.CentralBookingPagination.Add(new CentralBooking(string.Format("Development {0}", i), string.Format("Write a WPF program {0}", DateTime.Now.ToString()), 2, TaskType.Home));
                //var _task = _bookingList[i];
                //this.CentralBookingPagination[i].TaskName = _task.TaskName;
                //this.CentralBookingPagination[i].Description = _task.Description;
                //this.CentralBookingPagination[i].Priority = _task.Priority;
                //this.CentralBookingPagination[i].TaskType = _task.TaskType;
                //}
            }
            await Task.Delay(0);
            return;
        }
        public override void CustomPageTimerTick(object sender, EventArgs args)
        {
            this.CentralBookingPagination.RefreshDisplayColumn();
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
            new CentralBooking("C010", "10", 2, TaskType.Home),
            new CentralBooking("C011", "11", 2, TaskType.Home),
            new CentralBooking("C012", "12", 2, TaskType.Home),
            new CentralBooking("C013", "13", 2, TaskType.Home),
            new CentralBooking("C014", "14", 2, TaskType.Home),
            new CentralBooking("C015", "15", 2, TaskType.Home),
            new CentralBooking("C016", "16", 2, TaskType.Home),
            new CentralBooking("C017", "17", 2, TaskType.Home),
            new CentralBooking("C018", "18", 2, TaskType.Home),
            new CentralBooking("C019", "19", 2, TaskType.Home),
            new CentralBooking("C020", "20", 2, TaskType.Home),
            new CentralBooking("C021", "21", 2, TaskType.Home),
            new CentralBooking("C022", "22", 2, TaskType.Home),
            new CentralBooking("C023", "23", 2, TaskType.Home),
            new CentralBooking("C024", "24", 2, TaskType.Home),
            new CentralBooking("C025", "25", 2, TaskType.Home),
            new CentralBooking("C026", "26", 2, TaskType.Home),
            new CentralBooking("C027", "27", 2, TaskType.Home),
            new CentralBooking("C028", "28", 2, TaskType.Home),
            new CentralBooking("C029", "29", 2, TaskType.Home),
            new CentralBooking("C030", "30", 2, TaskType.Home),
        };

        private List<CentralBooking> dummyData20List3 = new List<CentralBooking>()
        {
            new CentralBooking("C041", "1", 2, TaskType.Home),
            new CentralBooking("C042", "2", 2, TaskType.Home),
            new CentralBooking("C043", "3", 2, TaskType.Home),
            new CentralBooking("C044", "4", 2, TaskType.Home),
            new CentralBooking("C045", "5", 2, TaskType.Home),
            new CentralBooking("C046", "6", 2, TaskType.Home),
            new CentralBooking("C047", "7", 2, TaskType.Home),
            new CentralBooking("C048", "8", 2, TaskType.Home),
            new CentralBooking("C049", "9", 2, TaskType.Home),
            new CentralBooking("C050", "10", 2, TaskType.Home),
            new CentralBooking("C051", "11", 2, TaskType.Home),
            new CentralBooking("C052", "12", 2, TaskType.Home),
            new CentralBooking("C053", "13", 2, TaskType.Home),
            new CentralBooking("C054", "14", 2, TaskType.Home),
            new CentralBooking("C055", "15", 2, TaskType.Home),
            new CentralBooking("C056", "16", 2, TaskType.Home),
            new CentralBooking("C057", "17", 2, TaskType.Home),
            new CentralBooking("C058", "18", 2, TaskType.Home),
            new CentralBooking("C059", "19", 2, TaskType.Home),
            new CentralBooking("C060", "20", 2, TaskType.Home),
        };

        private List<CentralBooking> dummyData10List3 = new List<CentralBooking>()
        {
            new CentralBooking("C061", "1", 2, TaskType.Home),
            new CentralBooking("C062", "2", 2, TaskType.Home),
            new CentralBooking("C063", "3", 2, TaskType.Home),
            new CentralBooking("C064", "4", 2, TaskType.Home),
            new CentralBooking("C065", "5", 2, TaskType.Home),
            new CentralBooking("C066", "6", 2, TaskType.Home),
            new CentralBooking("C067", "7", 2, TaskType.Home),
            new CentralBooking("C068", "8", 2, TaskType.Home),
            new CentralBooking("C069", "9", 2, TaskType.Home),
            new CentralBooking("C070", "10", 2, TaskType.Home),
        };

        public Task<List<CentralBooking>> GetAllData()
        {
            List<CentralBooking> _taskList = new List<CentralBooking>();
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
            return Task.FromResult<List<CentralBooking>>(_taskList);

            Char[] ticketType = new Char[] { 'S', 'C' };
            Char[] roomType = new Char[] { ' ', 'S' };
            for (int i = 1; i <= 12; i++)
            {
                var _ticketNum = this.GetRandomInt(1, 99).ToString("D3");
                var _roomNum = this.GetRandomInt(1, 30).ToString();
                _taskList.Add(new CentralBooking(
                    string.Format("{0}{1}",
                        ticketType[this.GetRandomInt(0, ticketType.Length)], _ticketNum),
                    string.Format("{0}{1}",
                        roomType[this.GetRandomInt(0, roomType.Length)], _roomNum),
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
