using AutoMapper;
using demo_mah_wpf.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demo_mah_wpf.Service
{
    public class AutoMapperService : IAutoMapperService
    {
        private static readonly Lazy<AutoMapperService> lazyAnnounceService = new Lazy<AutoMapperService>(() => new AutoMapperService());

        public Mapper InitializeAutomapper()
        {
            //Provide all the Mapping Configuration
            var config = new MapperConfiguration(cfg =>
            {
                //Configuring Employee and EmployeeDTO
                cfg.CreateMap<CentralBooking, VoiceAnnouncement>();
                //Any Other Mapping Configuration ....
            });
            //Create an Instance of Mapper and return that Instance
            var mapper = new Mapper(config);
            return mapper;
        }

        public AutoMapperService GetInstance()
        {
            return lazyAnnounceService.Value;
        }
    }
}
