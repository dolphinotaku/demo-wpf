using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace demo_mah_wpf
{
    public class TaskListDataTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate
            SelectTemplate(object item, DependencyObject container)
        {
            if (item != null && item is CentralBooking)
            {
                var taskitem = (CentralBooking)item;
                var window = Application.Current.MainWindow;
                if (taskitem.Priority == 1)
                    return
                        window.FindResource("ImportantTaskTemplate") as DataTemplate;
                return
                    window.FindResource("MyTaskTemplate") as DataTemplate;
            }

            return null;
        }
    }
}
