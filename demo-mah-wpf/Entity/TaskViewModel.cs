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
        private ObservableCollection<Task> _Tasks;
        public ObservableCollection<Task> TaskCollection
        {
            get { return _Tasks; }
            set
            {
                _Tasks = value;
                this.SetProperty(ref _Tasks, value, () => TaskCollection);
            }
        }

        public TaskViewModel() : base()
        {
            this.TaskCollection = new ObservableCollection<Task>();

            //this.TaskCollection.Add(new Task("Laundry", "Do my Laundry", 2, TaskType.Home));
            //this.TaskCollection.Add(new Task("Email", "Email clients", 1, TaskType.Work));
            //this.TaskCollection.Add(new Task("Clean", "Clean my office", 3, TaskType.Work));
            //this.TaskCollection.Add(new Task("Dinner", "Get ready for family reunion", 1, TaskType.Home));
            //this.TaskCollection.Add(new Task("Proposals", "Review new budget proposals", 2, TaskType.Work));

            // create 12 empty object
            IEnumerable<Task> _taskDataResult = (IEnumerable<Task>)this.CreateEmptyData(12);
            List<Task> _taskList = _taskDataResult.ToList();
            for (int i = 0; i < _taskList.Count; i++)
            {
                this.TaskCollection.Add(_taskList[i]);
            }
        }

        public override async void CustomTimerTick(object sender, EventArgs args)
        {
            if (this.currentSecond % this.defaultRefreshDataInSecond != 0) return;

            //this.TaskCollection.Clear();
            IEnumerable<Task> _taskDataResult = (IEnumerable<Task>)await this.GetAllData();
            List<Task> _taskList = _taskDataResult.ToList();
            if (_taskList != null && _taskList.Count > 0)
            {
                for (int i = 0; i < _taskList.Count; i++)
                {
                    //this.TaskCollection.Add(new Task(string.Format("Development {0}", i), string.Format("Write a WPF program {0}", DateTime.Now.ToString()), 2, TaskType.Home));
                    var _task = _taskList[i];
                    this.TaskCollection[i].TaskName = _task.TaskName;
                    this.TaskCollection[i].Description = _task.Description;
                    this.TaskCollection[i].Priority = _task.Priority;
                    this.TaskCollection[i].TaskType = _task.TaskType;
                }
            }
        }

        public System.Threading.Tasks.Task<List<Task>> GetAllData()
        {
            List<Task> _taskList = new List<Task>();
            for(int i = 1; i <= 12; i++)
            {
                _taskList.Add(new Task(string.Format("Development {0}", i), string.Format("Write a WPF program {0}", DateTime.Now.ToString()), 2, TaskType.Home));
            }

            return System.Threading.Tasks.Task.FromResult<List<Task>>(_taskList);
        }


        public List<Task> CreateEmptyData(int rowsCount = 12)
        {
            List<Task> _taskList = new List<Task>();
            for (int i = 1; i <= 12; i++)
            {
                _taskList.Add(new Task(string.Format("Empty {0}", i), string.Format("Description {0}", DateTime.Now.ToString()), 2, TaskType.Home));
            }

            return _taskList;
        }
    }
}
