using AutoMapper;
using demo_mah_wpf.Master;

namespace demo_mah_wpf.Service
{
    public class AutoMapperService : IAutoMapperService
    {
        public Mapper Instance { get; set; }

        public AutoMapperService()
        {
            var config = new MapperConfiguration(cfg =>
            {
                //Configuring Employee and EmployeeDTO
                cfg.CreateMap<CentralBooking, VoiceAnnouncement>();
                cfg.CreateMap<WalkInBooking, VoiceAnnouncement>();
                cfg.CreateMap<BusBooking, VoiceAnnouncement>();
                //Any Other Mapping Configuration ....
            });
            //Create an Instance of Mapper and return that Instance
            Instance = new Mapper(config);
        }
    }
}
