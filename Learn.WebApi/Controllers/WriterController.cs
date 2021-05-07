using AutoMapper;
using Learn.IService;
using Learn.Model;
using Learn.Model.DTO;
using Learn.Utility._MD5;
using Learn.Utility.ApiResult;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learn.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WriterController : ControllerBase
    {
        private readonly IWriterService _iWriterService;
        public WriterController(IWriterService iWriterService)
        {
            this._iWriterService = iWriterService;
        }


        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("Get")]
        public async Task<ActionResult<ApiResult>> GetWriter([FromServices] IMapper iMapper)
        {
            int id = Convert.ToInt32(this.User.FindFirst("Id").Value);
            var writer = await _iWriterService.FindAsync(id);
            var writerDTO = iMapper.Map<WriterDTO>(writer);
            if (writer == null) return ApiResultHelper.Error("没有该用户");
            return ApiResultHelper.Success(writer);
        }


        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="username"></param>
        /// <param name="userpwd"></param>
        /// <param name="nickname"></param>
        /// <param name="sex"></param>
        /// <param name="telnumber"></param>
        /// <returns></returns>
        [HttpPost("Create")]
        public async Task<ApiResult> Create(string username,string userpwd,string nickname,string sex,string telnumber)
        {
            byte sex1 = 0;
            if (sex == "女") sex1 = 1;
            Writer writer = new Writer
            {
                NickName = nickname,
                Sex = sex1,
                TelNumber = telnumber,
                UserName = username,
                //MD5加密
                LoginKey = MD5Helper.MD5Encrypt32(userpwd)
            };
            //判断数据库中是否已经存在账号跟要添加的账号相同的数据
            var oldWriter = await _iWriterService.FindAsync(c => c.UserName == username);
            if (oldWriter != null) return ApiResultHelper.Error("账号已经存在");
            bool b = await _iWriterService.CreateAsync(writer);
            if (!b) return ApiResultHelper.Error("添加失败");
            return ApiResultHelper.Success(writer);
        }


        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="nickname"></param>
        /// <param name="sex"></param>
        /// <param name="telnumber"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut("Edit")]
        public async Task<ApiResult> Edit(string nickname, string sex, string telnumber)
        {
            byte sex1 = 0;
            int id = Convert.ToInt32(this.User.FindFirst("Id").Value);
            var writer = await _iWriterService.FindAsync(id);
            if (sex == "女") sex1 = 1;
            writer.NickName = nickname;
            writer.Sex = sex1;
            writer.TelNumber = telnumber;
            bool b = await _iWriterService.EditAsync(writer);
            if (!b) return ApiResultHelper.Error("修改失败");
            return ApiResultHelper.Success(writer);
        }

    }
}
