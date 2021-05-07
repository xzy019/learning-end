using Learn.IService;
using Learn.Model;
using Learn.JWT.Utility.ApiResult;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Learn.JWT.Utility._MD5;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace Learn.JWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthoizeController : ControllerBase
    {
        private readonly IWriterService _iWriterService;
        public AuthoizeController(IWriterService iWriterService)
        {
            _iWriterService = iWriterService;
        }
        [HttpPost("Login")]
        public async Task<ApiResult> Login(string username,string userpwd)
        {
            string pwd = MD5Helper.MD5Encrypt32(userpwd);
            var writer = await _iWriterService.FindAsync(c => c.UserName == username && c.LoginKey == pwd);
            if (writer != null)
            {
                //登陆成功
                var claims = new Claim[]
            {
                new Claim(ClaimTypes.Name, writer.NickName),
                new Claim("ID",writer.ID.ToString()),
                new Claim("UserName",writer.UserName),
                //不能放敏感信息
            };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SDMC-CJAS1-SAD-DFSFA-SADHJVF-VF"));
                //issuer代表颁发Token的Web应用程序，audience是Token的受理者
                var token = new JwtSecurityToken(
                    issuer: "http://localhost:6060",
                    audience: "http://localhost:5000",
                    claims: claims,
                    notBefore: DateTime.Now,
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );
                var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
                return ApiResultHelper.Success(jwtToken);
            }
            else
            {
                //登录失败
                return ApiResultHelper.Error("账号或密码错误");
            }
        }
    }
}
