using AutoMapper;
using BL.Interfaces;
using BL.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Bases
{
    public class Base_ApplicationService : IDisposable
    {

        protected IUnitOfWork TheUnitOfWork { get; set; }
        protected readonly IMapper Mapper; 
       

        public Base_ApplicationService(IUnitOfWork _UnitOfWork, IMapper mapper)
        {
            TheUnitOfWork = _UnitOfWork;
            Mapper = mapper;
        }

        public void Dispose()
        {
            TheUnitOfWork.Dispose();
        }
    }
}
