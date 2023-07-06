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
    public class TaskViewModel : FetchDataViewModel
    {
        //private LinkedList<CentralBooking> _tupleList;
        //private ObservableCollection<CentralBooking> column1BookList;
        //private ObservableCollection<CentralBooking> column2BookList;
        //private int _currentPage;
        //private int _totalPage;
        //private int _defaultPageRowCount;
        protected CentralBooking firstDisplayBookingNode;
        protected CentralBooking lastDisplayBookingNode;

        private ObservableCollection<CentralBookingPagination> _TaskCollection1;
        public ObservableCollection<CentralBookingPagination> CentralBookingPagination
        {
            get { return _TaskCollection1; }
            set
            {
                _TaskCollection1 = value;
                this.SetProperty(ref _TaskCollection1, value, () => CentralBookingPagination);
            }
        }

        public IEnumerable<CentralBooking> taskQueryList;

        //public ReadOnlyObservableCollection<CentralBooking> GetPage()
        //{

        //}

        public TaskViewModel() : base()
        {
            this.taskQueryList = new List<CentralBooking>();
            this.CentralBookingPagination = new ObservableCollection<CentralBookingPagination>();
            //this.TaskCollectionPagination = new ObservableCollection<CentralBooking>();

            //this.CentralBookingPagination.Add(new CentralBooking("Laundry", "Do my Laundry", 2, TaskType.Home));
            //this.CentralBookingPagination.Add(new CentralBooking("Email", "Email clients", 1, TaskType.Work));
            //this.CentralBookingPagination.Add(new CentralBooking("Clean", "Clean my office", 3, TaskType.Work));
            //this.CentralBookingPagination.Add(new CentralBooking("Dinner", "Get ready for family reunion", 1, TaskType.Home));
            //this.CentralBookingPagination.Add(new CentralBooking("Proposals", "Review new budget proposals", 2, TaskType.Work));

            // create two column
            //for (int i = 0; i < 1; i++)
            //{
                CentralBookingPagination _bookingPage = new CentralBookingPagination();

                // add empty 6 rows
                IEnumerable<CentralBooking> _taskDataResult = (IEnumerable<CentralBooking>)this.CreateEmptyData(6);
                List<CentralBooking> _taskList = _taskDataResult.ToList();
                _bookingPage.AddBookingRange(_taskList);
                this.CentralBookingPagination.Add(_bookingPage);
            //}
        }

        public override async void CustomDataTimerTick(object sender, EventArgs args)
        {
            if (this.currentSecond % this.RefaultRefreshDataInEverySecond != 0) return;

            //this.CentralBookingPagination.Clear();
            IEnumerable<CentralBooking> _taskDataResult = (IEnumerable<CentralBooking>)await this.GetAllData();
            List<CentralBooking> _bookingList = _taskDataResult.ToList();
            if (_bookingList != null && _bookingList.Count > 0)
            {
                this.CentralBookingPagination[0].ClearBookingList();
                this.CentralBookingPagination[0].AddBookingRange(_bookingList);
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
            for(int i = 1; i <= 12; i++)
            {
                _taskList.Add(new CentralBooking(string.Format("Development {0}", i), string.Format("Write a WPF program {0}", DateTime.Now.ToString()), 2, TaskType.Home));
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
