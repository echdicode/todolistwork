using Microsoft.AspNetCore.Mvc;
using todolistwork.Api.Authentication;
using todolistwork.Api.Filter;
using todolistwork.Application.IService;
using todolistwork.Core.Entities;
using todolistwork.Core.Models;

namespace todolistwork.Api.Controllers
{
    [Route("api/admin/user")]
    [TypeFilter(typeof(AdminFilter))]

    [ApiController]

    public class AdminController : ControllerBase
    {
        private readonly IUserService UserService;
        private readonly JwtService jwtService;
        public AdminController(IUserService userService)
        {
            UserService = userService;
        }


        // GET: api/<ValuesController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try {
                var result = await UserService.GetAllAsync();
                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            try {
            
                var result = await UserService.GetProfile(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

       

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] ChangeProfile data)
        { 
            try {
                User user= new User();
                user.Id = id;
                user.UserName = data.UserName;

                var result = await UserService.ChangeProfile(user);
                return Ok(result);


            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try {
              

                var result = await UserService.DeleteAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
