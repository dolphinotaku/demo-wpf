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
using demo_mah_wpf.Service;

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
        private BusBookingViewModel _bookingListC;
        public BusBookingViewModel BusBookingViewModel
        {
            get { return _bookingListC; }
            set
            {
                _bookingListC = value;
            }
        }



        public ViewModel(IVoiceService voiceService, IAutoMapperService autoMapperService) : base()
        {
            VoiceService _voiceService = voiceService.GetInstance();
            //BindingOperations.EnableCollectionSynchronization(TaskCollection1, _lock);

            this.CentralBookingViewModel = new CentralBookingViewModel(_voiceService, autoMapperService);
            this.WalkInBookingViewModel = new WalkInBookingViewModel(autoMapperService);
            this.BusBookingViewModel = new BusBookingViewModel(autoMapperService);

            // use async in .net framework 4.7.2
            // otherwise, complie error: async streams is not available in 7.3
            // https://bartwullems.blogspot.com/2020/01/asynchronous-streams-using.html

            //Task.Run(async () => {
            //    var getTaskResult = this.GetAllTasks();
            //});
        }

    }
}
