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
    public class StudyService : BaseService<StudyInfo>, IStudyService
    {
        private readonly IStudyRepository _iStudyRespository;
        public StudyService(IStudyRepository iStudyRespository)
        {
            base._iBaseRepository = iStudyRespository;
            _iStudyRespository = iStudyRespository;
        }
    }
}