using Learn.IRepository;
using Learn.IService;
using Learn.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn.Service
{
    public class WriterService:BaseService<Writer>,IWriterService
    {
        private readonly IWriterRepository _iWriterRepository;
        public WriterService(IWriterRepository iWriterRepository)
        {
            base._iBaseRepository = iWriterRepository;
            _iWriterRepository = iWriterRepository;
        }
    }
}
