using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace demo_mah_wpf
{
    public class BusBookingPaginationDataTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate
            SelectTemplate(object item, DependencyObject container)
        {
            if (item != null && item is BusBooking)
            {
                var taskitem = (BusBooking)item;
                var window = Application.Current.MainWindow;
                if (!string.IsNullOrEmpty(taskitem.Description) && taskitem.Description.Length >= 45)
                    return
                        window.FindResource("DoubleHeightBusBookingTemplate") as DataTemplate;
                return
                    window.FindResource("SingleHeightBusBookingTemplate") as DataTemplate;
            }

            return null;
        }
    }
}
