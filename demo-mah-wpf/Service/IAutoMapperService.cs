using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demo_mah_wpf.Service
{
    public interface IAutoMapperService
    {
        public Mapper InitializeAutomapper();
        public AutoMapperService GetInstance();
    }
}