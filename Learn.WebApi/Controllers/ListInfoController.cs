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
    public class ListInfoController : ControllerBase
    {
        private readonly IListService _iListService;
        public ListInfoController(IListService iListService)
        {
            this._iListService = iListService;
        }

        /// <summary>
        /// 用户创建清单任务
        /// </summary>
        /// <param name="title"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        [HttpPost("Create")]
        public async Task<ApiResult> Create(string title, byte level)
        {
            ListInfo List = new ListInfo
            {
                Title = title,
                Level = level,
                Complete = 0,
                WriterID = Convert.ToInt32(this.User.FindFirst("Id").Value)
            };
            bool b = await _iListService.CreateAsync(List);
            if (!b) return ApiResultHelper.Error("添加失败");
            return ApiResultHelper.Success(List);
        }

        /// <summary>
        /// 用户删除清单任务
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("Detele")]
        public async Task<ActionResult<ApiResult>> Delete(int id)
        {
            bool b = await _iListService.DeleteAsync(id);
            if (!b) return ApiResultHelper.Error("删除失败");
            return ApiResultHelper.Success(b);
        }

        /// <summary>
        /// 用户获取自己的清单任务
        /// </summary>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        [HttpGet("Get")]
        public async Task<ApiResult> Get(int page, int size)
        {
            RefAsync<int> total = 0;
            int id = Convert.ToInt32(this.User.FindFirst("Id").Value);
            var list = await _iListService.QueryAsync(c=>c.WriterID==id,page, size, total);
            if (list == null) return ApiResultHelper.Error("没有找到数据");
            return ApiResultHelper.Success(list, total);
        }

        /// <summary>
        /// 用户完成清单任务
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("Edit")]
        public async Task<ApiResult> Edit(int id)
        {
            var list = await _iListService.FindAsync(id);
            if (list == null) return ApiResultHelper.Error("没有找到该数据");
            list.Complete = 1;
            bool b = await _iListService.EditAsync(list);
            if (!b) return ApiResultHelper.Error("修改失败");
            return ApiResultHelper.Success(list);
        }
    }
}
