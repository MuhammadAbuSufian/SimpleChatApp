using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ChatApi.Models.RequestModels;
using ChatApi.Services;
using DotNetCoreApiStarter.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace ChatApi.Controllers
{
    [Route("api/account")]
    [ApiController]

    public class UserController : ControllerBase
    {
       
        private readonly IUserService _service;

        const string password = "default123@^";

        public UserController(IUserService service)
        {
            _service = service;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAll();
            return Ok(result);
        }

        [HttpPost]
        [Route("Register")]
        //POST : /api/User/Register
        public async Task<Object> Register(UserCreateRequestModel model)
        {
            var newUser = new ApplicationUser();
            newUser.FirstName = model.FirstName;
            newUser.LastName = model.LastName;
            newUser.Email = model.Email;
            newUser.IsOnline = model.IsOnline;
            var emailAlreadyExist = await _service.GetByEmail(newUser.Email);
            if(emailAlreadyExist != null)
            {
                return Ok(null);
            }
            return Ok(await _service.Save(newUser));
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginRequestModel model)
        {
            var user = await _service.GetByEmail(model.Email);
            if (user != null)
            {
                try
                {
                    IdentityOptions _options = new IdentityOptions();
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                        new Claim("UserID",user.Id.ToString()),
                        new Claim("Email",user.Email),
                        new Claim("FullName",user.FirstName+" "+user.LastName),
                        }),
                        Expires = DateTime.UtcNow.AddDays(1),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("1234567890123456")), SecurityAlgorithms.HmacSha256Signature)
                    };
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                    var token = tokenHandler.WriteToken(securityToken);
                    user.IsOnline = true;
                    return Ok(new { token, user });
                }
                catch (Exception exp)
                {
                    return BadRequest(exp);
                }
            }
            else
                return BadRequest(new { message = "Username or password is incorrect." });
        }
    }
}
