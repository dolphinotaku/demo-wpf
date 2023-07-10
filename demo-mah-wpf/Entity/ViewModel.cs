using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;
using System.Windows.Data;
using System.ComponentModel;
using demo_mah_wpf.Entity;
using System.Windows.Threading;

namespace demo_mah_wpf
{
    public class ViewModel
    {
        private CentralBookingViewModel _bookingListA;
        public CentralBookingViewModel CentralBookingViewModel
        {
            get { return _bookingListA; }
            set
            {
                _bookingListA = value;
            }
        }
        private WalkInBookingViewModel _bookingListB;
        public WalkInBookingViewModel WalkInBookingViewModel
        {
            get { return _bookingListB; }
            set
            {
                _bookingListB = value;
            }
        }

        

        public ViewModel() : base()
        {
            //BindingOperations.EnableCollectionSynchronization(TaskCollection1, _lock);

            this.CentralBookingViewModel = new CentralBookingViewModel();
            this.WalkInBookingViewModel = new WalkInBookingViewModel();

            // use async in .net framework 4.7.2
            // otherwise, complie error: async streams is not available in 7.3
            // https://bartwullems.blogspot.com/2020/01/asynchronous-streams-using.html

            //Task.Run(async () => {
            //    var getTaskResult = this.GetAllTasks();
            //});
        }
    }
}
