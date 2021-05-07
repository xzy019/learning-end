using Learn.IService;
using Learn.Model;
using Learn.Utility.ApiResult;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Learn.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NoteInfoController : ControllerBase
    {
        private readonly INoteService _iNoteService;
        public readonly IWebHostEnvironment _webHostEnvironment;
        public NoteInfoController(INoteService iNoteService, IWebHostEnvironment webHostEnvironment)
        {
            this._iNoteService = iNoteService;
            _webHostEnvironment = webHostEnvironment;
        }

        /// <summary>
        /// 创建笔记
        /// </summary>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <param name="tag"></param>
        /// <returns></returns>
        [HttpPost("Create")]
        public async Task<ApiResult> Create(string title,string content,string tag)
        {
            NoteInfo note = new NoteInfo
            {
                Title = title,
                Content = content,
                Time = DateTime.Now,
                Tag = tag,
                LikeNumber = 0,
                WriterID = Convert.ToInt32(this.User.FindFirst("Id").Value)
            };
            bool b = await _iNoteService.CreateAsync(note);
            if (!b) return ApiResultHelper.Error("添加失败");
            return ApiResultHelper.Success(note);
        }


        /// <summary>
        /// 用户删除自己的笔记
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("Detele")]
        public async Task<ActionResult<ApiResult>> Delete(int id)
        {
            bool b = await _iNoteService.DeleteAsync(id);
            if (!b) return ApiResultHelper.Error("删除失败");
            return ApiResultHelper.Success(b);
        }

        /// <summary>
        /// 用户编辑自己的笔记
        /// </summary>
        /// <param name="id"></param>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <param name="tag"></param>
        /// <returns></returns>
        [HttpPut("Edit")]
        public async Task<ActionResult<ApiResult>> Edit(int id, string title, string content, string tag)
        {
            var note = await _iNoteService.FindAsync(id);
            if (note == null) return ApiResultHelper.Error("没有找到该数据");
            note.Title = title;
            note.Content = content;
            note.Tag = tag;
            bool b = await _iNoteService.EditAsync(note);
            if (!b) return ApiResultHelper.Error("修改失败");
            return ApiResultHelper.Success(note);
        }

        /// <summary>
        /// 查找所有笔记
        /// </summary>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        [HttpGet("NotePageAll")]
        public async Task<ApiResult> GetNotePageAll(int page, int size)
        {
            RefAsync<int> total = 0;
            var note = await _iNoteService.QueryAsync(page, size, total);
            if (note == null) return ApiResultHelper.Error("没有找到数据");
            return ApiResultHelper.Success(note, total);
        }

        /// <summary>
        /// 根据标签查找所有笔记
        /// </summary>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <param name="tag"></param>
        /// <returns></returns>
        [HttpGet("NotePageTag")]
        public async Task<ApiResult> GetNotePageTag(int page, int size,string tag)
        {
            RefAsync<int> total = 0;
            var note = await _iNoteService.QueryAsync(c=>c.Tag==tag, page, size, total);
            if(note ==null) return ApiResultHelper.Error("没有找到数据");
            return ApiResultHelper.Success(note, total);
        }


        /// <summary>
        /// 查找自己所写的笔记
        /// </summary>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        [HttpGet("NotePage")]
        public async Task<ApiResult> GetNotePage(int page, int size)
        {
            RefAsync<int> total = 0;
            int id = Convert.ToInt32(this.User.FindFirst("Id").Value);
            var note = await _iNoteService.QueryAsync(c=>c.WriterID==id,page, size, total);
            if (note == null) return ApiResultHelper.Error("没有找到数据");
            return ApiResultHelper.Success(note, total);
        }

        [HttpGet("NoteID")]
        public async Task<ApiResult> GetNoteID(int id)
        {
            var note = await _iNoteService.QueryAsync(c => c.ID == id);
            if (note == null) return ApiResultHelper.Error("没有找到数据");
            return ApiResultHelper.Success(note);
        }

        /// <summary>
        /// 点赞笔记
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("Like")]
        public async Task<ActionResult<ApiResult>> Like(int id)
        {
            var note = await _iNoteService.FindAsync(id);
            if (note == null) return ApiResultHelper.Error("没有找到该数据");
            note.LikeNumber = ++note.LikeNumber;
            bool b = await _iNoteService.EditAsync(note);
            if (!b) return ApiResultHelper.Error("点赞失败");
            return ApiResultHelper.Success(note);
        }

        [HttpPost("UpLoadImg")]
        public async Task<ApiResult> UpLoadImg([FromForm(Name = "imgFile")]IFormFile file)
        {
            string webRootPath = _webHostEnvironment.WebRootPath; // wwwroot 文件夹
            string uploadPath = Path.Combine("uploads", DateTime.Now.ToString("yyyyMMdd"));
            string dirPath = Path.Combine(webRootPath, uploadPath);
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
            string fileExt = Path.GetExtension(file.FileName).Trim('.'); //文件扩展名，不含“.”
            string newFileName = Guid.NewGuid().ToString().Replace("-", "") + "." + fileExt; //随机生成新的文件名
            var fileFolder = Path.Combine(dirPath, newFileName);
            using (var stream = new FileStream(fileFolder, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            string url = $@"/{uploadPath}/{newFileName}";
            return ApiResultHelper.Success("http://localhost:5000"+url);
        }

    }
}
