using demo_mah_wpf.Entity;
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


        public WalkInBookingViewModel() : base()
        {
            this.taskQueryList = new List<WalkInBooking>();
            this.WalkInBookingPagination = new WalkInBookingPagination();
            //this.TaskCollectionPagination = new ObservableCollection<WalkInBooking>();

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
            if (this.currentSecond % this.RefaultRefreshDataInEverySecond != 0) return;

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

        public Task<List<WalkInBooking>> GetAllData()
        {
            List<WalkInBooking> _taskList = new List<WalkInBooking>();
            Char[] ticketType = new Char[] { 'S', 'C', 'W' };
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
