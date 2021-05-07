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
    public class NoteService : BaseService<NoteInfo>, INoteService
    {
        private readonly INoteRepository _iNoteRespository;
        public NoteService(INoteRepository iNoteRespository)
        {
            base._iBaseRepository = iNoteRespository;
            _iNoteRespository = iNoteRespository;
        }
    }
}