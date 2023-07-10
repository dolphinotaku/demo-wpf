using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace demo_mah_wpf
{
    public class BookingPaginationDataTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate
            SelectTemplate(object item, DependencyObject container)
        {
            if (item != null && item is Booking)
            {
                var taskitem = (Booking)item;
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
