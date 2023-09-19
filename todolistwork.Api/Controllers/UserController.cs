using Microsoft.AspNetCore.Mvc;
using todolistwork.Core.Entities;
using todolistwork.Core.Models;
using todolistwork.Core.Unit;
using todolistwork.Application.IService;

namespace todolistwork.Api.Controllers
{
    [Route("api/user")]
    public class UserController : BaseApiController
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }
        [HttpPut("change_password")]
        public IActionResult change_password([FromBody] ChangePasswordBody databody)
        {

            try
            {
                
                User user = new User();
                user.Id = HttpContext.Items["Id"]?.ToString();
                user.Email = HttpContext.Items["Email"]?.ToString();
                user.Password = UnitCore.HashMd5(databody.NewPassword);
                var data = userService.ChangePassword(user);
                if (data == null)
                {
                    return BadRequest();
                }
                return Ok();
            }

            catch (Exception ex)
            {
                return BadRequest(ex);

            }

           
        }
        [HttpGet("profile")]
        public async Task<IActionResult> profile()
        {

            try
            {
                User user = new User();
         
                string id= HttpContext.Items["Id"]?.ToString();
                var result = await userService.GetProfile(id);
              
                return Ok(result);
            }

            catch (Exception ex)
            {
                return BadRequest(ex);

            }

           
        }
        [HttpPut("profile")]
        public async Task<IActionResult> changeProfile([FromBody] ChangeProfile usernameBody)
        {

            try
            {
                User user = new User();
                user.Id = HttpContext.Items["Id"]?.ToString();
                user.Email = HttpContext.Items["Email"]?.ToString();
                user.UserName = usernameBody.UserName;
                var result = await userService.ChangeProfile(user);
                if (result == null)
                {
                    return BadRequest();
                }
                return Ok(result);
            }

            catch (Exception ex)
            {
                return BadRequest(ex);

            }


        }


    }
}
