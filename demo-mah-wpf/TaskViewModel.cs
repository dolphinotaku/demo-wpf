using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Windows.Data;
using System.ComponentModel;
using demo_mah_wpf.Entity;
using System.Windows.Threading;

namespace demo_mah_wpf
{
    public class TaskViewModel
    {

        public ObservableCollection<Task> TaskCollection { get; }
            = new ObservableCollection<Task>();
        private readonly DispatcherTimer timer = new DispatcherTimer();
        private readonly Random random = new Random();

        private static object _lock = new object();
        public TaskViewModel() : base()
        {
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += TimerTick;
            timer.Start();

            //BindingOperations.EnableCollectionSynchronization(TaskCollection, _lock);


            // use async in .net framework 4.7.2
            // otherwise, complie error: async streams is not available in 7.3
            // https://bartwullems.blogspot.com/2020/01/asynchronous-streams-using.html

            //System.Threading.Tasks.Task.Run(async () => {
            //    var getTaskResult = this.GetAllTasks();
            //    if(getTaskResult != null && getTaskResult.Result != null)
            //    {
            //        var getTaskResultList = getTaskResult.Result.ToList();
            //        if (getTaskResultList.Count != 0)
            //        {
            //            foreach (Task task in getTaskResultList)
            //            {
            //                //Add(new Task(task.TaskName, task.Description, task.Priority, task.TaskType));
            //                this.Tasks.Add(task);
            //                CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add));
            //            }
            //            //Add(task);

            //        }
            //    }
            //});
        }

        private async System.Threading.Tasks.Task<IEnumerable<Task>> GetAllTasks()
        {
            List<Task> TaskCollection = new List<Task>();
            for (int i = 1; i <= 3; i++)
            {
                await System.Threading.Tasks.Task.Delay(1000);
                TaskCollection.Add(new Task(string.Format("Development {0}",i+1), "Write a WPF program", 2, TaskType.Home));
            }
            return TaskCollection;
        }
        private async void TimerTick(object sender, EventArgs args)
        {
            this.TaskCollection.Clear();
            await System.Threading.Tasks.Task.Delay(1000);
            for (int i = 1; i <= 10; i++)
            {
                this.TaskCollection.Add(new Task(string.Format("Development {0}", i + 1), "Write a WPF program", 2, TaskType.Home));
            }
            //this.Tasks.Add(new Item { ItemData = $"Item Data: {random.Next(100)}" });
            //this.Tasks.Add(new Item { ItemData = $"Item Data: {random.Next(100)}" });
            //this.Tasks.Add(new Item { ItemData = $"Item Data: {random.Next(100)}" });
            //this.Tasks.Add(new Item { ItemData = $"Item Data: {random.Next(100)}" });
        }
    }
}
