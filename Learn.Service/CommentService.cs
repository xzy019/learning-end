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
    public class CommentService:BaseService<CommentInfo>,ICommentService
    {
        private readonly ICommentRepository _iCommentRespository;
        public CommentService(ICommentRepository iCommentRespository)
        {
            base._iBaseRepository = iCommentRespository;
            _iCommentRespository = iCommentRespository;
        }
    }
}
