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



        public ViewModel(IVoiceService voiceService, IAutoMapperService autoMapperService)
        {
            this.CentralBookingViewModel = new CentralBookingViewModel(voiceService, autoMapperService);
            this.WalkInBookingViewModel = new WalkInBookingViewModel(voiceService, autoMapperService);
            this.BusBookingViewModel = new BusBookingViewModel(voiceService, autoMapperService);
        }

    }
}
