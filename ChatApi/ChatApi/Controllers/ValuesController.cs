using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCoreApiStarter.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCoreApiStarter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        //private readonly IUserService _service;
        public ValuesController()
        {
            //_service = service;
        }
        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            //var users = await _service.GetAll();
            return Ok("");
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post()
        {
            //var user = new User();
            //user.Name = "Sufian";
            //_service.Add(user);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
