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
    [Authorize]
    public class StudyInfoController : ControllerBase
    {
        private readonly IStudyService _iStudyService;
        public StudyInfoController(IStudyService iStudyService)
        {
            this._iStudyService = iStudyService;
        }

        /// <summary>
        /// 用户创建学习记录
        /// </summary>
        /// <param name="studytime"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        [HttpPost("Create")]
        public async Task<ApiResult> Create(string studytime, string type)
        {
            StudyInfo Study = new StudyInfo
            {
                StudyTime = studytime,
                Type = type,
                WriterID = Convert.ToInt32(this.User.FindFirst("Id").Value)
            };
            bool b = await _iStudyService.CreateAsync(Study);
            if (!b) return ApiResultHelper.Error("添加失败");
            return ApiResultHelper.Success(Study);
        }

        /// <summary>
        /// 用户删除学习记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("Delete")]
        public async Task<ActionResult<ApiResult>> Delete(int id)
        {
            bool b = await _iStudyService.DeleteAsync(id);
            if (!b) return ApiResultHelper.Error("删除失败");
            return ApiResultHelper.Success(b);
        }

        /// <summary>
        /// 用户编辑自己的学习记录
        /// </summary>
        /// <param name="id"></param>
        /// <param name="studytime"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        [HttpPut("Edit")]
        public async Task<ApiResult> Edit(int id, string studytime, string type)
        {
            var study = await _iStudyService.FindAsync(id);
            if (study == null) return ApiResultHelper.Error("没有找到该数据");
            study.StudyTime = studytime;
            study.Type = type;
            bool b = await _iStudyService.EditAsync(study);
            if (!b) return ApiResultHelper.Error("修改失败");
            return ApiResultHelper.Success(study);
        }

        /// <summary>
        /// 用户获取自己所有的学习记录
        /// </summary>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        [HttpGet("Get")]
        public async Task<ApiResult> Get(int page, int size)
        {
            RefAsync<int> total = 0;
            int id = Convert.ToInt32(this.User.FindFirst("Id").Value);
            var study = await _iStudyService.QueryAsync(c=>c.WriterID==id,page, size, total);
            if (study == null) return ApiResultHelper.Error("没有找到数据");
            return ApiResultHelper.Success(study, total);
        }
    }
}
