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
    public class CentralBookingViewModel : FetchDataViewModel
    {
        protected CentralBooking firstDisplayBookingNode;
        protected CentralBooking lastDisplayBookingNode;

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


        public CentralBookingViewModel() : base()
        {
            this.taskQueryList = new List<CentralBooking>();
            this.CentralBookingPagination = new CentralBookingPagination();
            //this.TaskCollectionPagination = new ObservableCollection<CentralBooking>();

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
            if (this.currentSecond % this.RefaultRefreshDataInEverySecond != 0) return;

            //this.CentralBookingPagination.Clear();
            IEnumerable<CentralBooking> _taskDataResult = (IEnumerable<CentralBooking>)await this.GetAllData();
            List<CentralBooking> _bookingList = _taskDataResult.ToList();
            if (_bookingList != null && _bookingList.Count > 0)
            {
                this.CentralBookingPagination.ClearBookingList();
                this.CentralBookingPagination.AddBookingRange(_bookingList);
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

        public Task<List<CentralBooking>> GetAllData()
        {
            List<CentralBooking> _taskList = new List<CentralBooking>();
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
