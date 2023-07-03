using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demo_mah_wpf
{
    public class TaskList : ObservableCollection<Task>
    {
        public TaskList()
        {
            //Add(new Task("Groceries", "Pick up Groceries and Detergent", 2, TaskType.Home));
            Add(new Task("Laundry", "Do my Laundry", 2, TaskType.Home));
            Add(new Task("Email", "Email clients", 1, TaskType.Work));
            Add(new Task("Clean", "Clean my office", 3, TaskType.Work));
            Add(new Task("Dinner", "Get ready for family reunion", 1, TaskType.Home));
            Add(new Task("Proposals", "Review new budget proposals", 2, TaskType.Work));
        }
    }
}
