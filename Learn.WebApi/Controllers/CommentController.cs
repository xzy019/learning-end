using Learn.IService;
using Learn.Model;
using Learn.Utility.ApiResult;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learn.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _iCommentService;
        public CommentController(ICommentService iCommentService)
        {
            this._iCommentService = iCommentService;
        }

        /// <summary>
        /// 查看笔记的评论
        /// </summary>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Get")]
        public async Task<ApiResult> Get(int page, int size,int id)
        {
            RefAsync<int> total = 0;
            var comment = await _iCommentService.QueryAsync(c => c.NoteID == id,page, size, total);
            if (comment == null) return ApiResultHelper.Error("没有数据");
            return ApiResultHelper.Success(comment, total);
        }


        /// <summary>
        /// 发表评论
        /// </summary>
        /// <param name="comment"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("Create")]
        public async Task<ApiResult> Create(string comment,int id)
        {
            CommentInfo Comment = new CommentInfo
            {
                WriterID = Convert.ToInt32(this.User.FindFirst("Id").Value),
                NoteID = id,
                Comment = comment
            };
            bool b = await _iCommentService.CreateAsync(Comment);
            if (!b) return ApiResultHelper.Error("添加失败");
            return ApiResultHelper.Success(Comment);
        }
    }
}
