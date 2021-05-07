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
    public class ListService : BaseService<ListInfo>, IListService
    {
        private readonly IListRepository _iListRespository;
        public ListService(IListRepository iListRespository)
        {
            base._iBaseRepository = iListRespository;
            _iListRespository = iListRespository;
        }
    }
}