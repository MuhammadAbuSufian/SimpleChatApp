using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ChatApi.Models;
using ChatApi.Models.RequestModels;
using ChatApi.Services;
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

        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAll());
        }

        [HttpPost]
        [Route("Register")]
        //POST : /api/User/Register
        public async Task<IActionResult> Register(UserCreateRequestModel model)
        {
            var createUSer = new User();
            createUSer.FirstName = model.FirstName;
            createUSer.LastName = model.LastName;
            createUSer.Email = model.Email;
            createUSer.IsOnline = model.IsOnline;
            var emailAlreadyExist = await _service.GetByEmail(createUSer.Email);
            if(emailAlreadyExist != null)
            {
                return Ok(null);
            }
            return Ok(await _service.Add(createUSer));
        }

        [HttpPost]
        [Route("Login")]
        //POST : /api/User/Login
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
                return BadRequest(new { message = "Email is Not Valid." });
        }
    }
}
