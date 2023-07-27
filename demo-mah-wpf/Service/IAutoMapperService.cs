using AutoMapper;

namespace demo_mah_wpf.Service
{
    public interface IAutoMapperService
    {
        public Mapper Instance { get; set; }
    }
}