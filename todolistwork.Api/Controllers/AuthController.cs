using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using todolistwork.Api.Authentication;
using todolistwork.Application.IService;
using todolistwork.Core.Entities;
using todolistwork.Core.Models;
using todolistwork.Core.Unit;
using todolistwork.Infrastructure.GoogleServer;
using Google.Apis.Auth;
using Newtonsoft.Json.Linq;
using todolistwork.Application.ICache;
using todolistwork.Infrastructure.database.Redis;
using System.Xml.Linq;

namespace CleanArch.Api.Controllers
{
    [Route("api/auth")]
    [ApiController]

    public class AuthController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IConfiguration _configuration;
        private readonly JwtService jwtService;
        private readonly IRedisService _redisService;

        public AuthController(IUserService userService, IConfiguration _configuration, IRedisService _redisService)
        {
            this.userService = userService;
            this._configuration = _configuration;
            this.jwtService = new JwtService(_configuration);
            this._redisService= _redisService;
        }
        [HttpPost("sign")]
        public async Task<IActionResult> Sign(string TokenId)
        {
            try
            {
                GoogleJsonWebSignature.ValidationSettings settings = new GoogleJsonWebSignature.ValidationSettings();

            


                GoogleJsonWebSignature.Payload payload = await GoogleJsonWebSignature.ValidateAsync(TokenId, settings);
             
                string email= payload.Email;
                var data = await userService.GetUserByEmail(email);
              

                if (data==null) {
                    User user = new User();
                    user.Email = payload.Email;
                    var pass = UnitCore.Random();
                    user.Password = UnitCore.HashMd5(pass);
                    user.UserName = payload.Name;
                    var resuit = await userService.Registration(user);

                    var resultGmali = await MailUtils.SendMailGoogleSmtp("ducthangpg@gmail.com", payload.Email, "MẬT KHẨU ĐĂNG NHẬP CỦA BẠN", pass);
                    var data1 = await userService.GetUserByEmail(email);
                    string token1 = await jwtService.GenerateTokenAuth(data1);
                    Response.Cookies.Append("tokenjwt", token1, new CookieOptions() { SameSite = SameSiteMode.None, Secure = true });

                    bool x = _redisService.SetData<string>("TokenUser:" + token1, payload.Email, DateTimeOffset.Now.AddHours(7));


                    if (!resultGmali)
                    {
                        return BadRequest();
                    }
                }
                else
                {
                    var tokenuser2 = await jwtService.GenerateToken(data);
                    Response.Cookies.Append("tokenjwt", tokenuser2, new CookieOptions() { SameSite = SameSiteMode.None, Secure = true });

                    bool x = _redisService.SetData<string>("TokenUser:" + tokenuser2, payload.Email, DateTimeOffset.Now.AddHours(7));



                }
                return Ok();
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return BadRequest(ex);

            }


        }

        [HttpPost("registration")]
        public async Task<IActionResult> registration([FromBody] RegistrationBody bodyModel)
        {

            try
            {
               

                
//                var data = await userService.GetUserByEmail(bodyModel.Email);
                 User data = new User(); 
                data.Email = bodyModel.Email;
                data.Password = bodyModel.Password;
                data.UserName = bodyModel.UserName;
                data.Id = "0";
                string token = await jwtService.GenerateTokenAuth(data);
                string url = string.Format(ContentMail.UrlToken, "registration", token);
                string contentMail = string.Format(ContentMail.Registration, url);
                var resultGmali = await MailUtils.SendMailGoogleSmtp("ducthangpg@gmail.com", bodyModel.Email, "XÁC THỰC ĐĂNG KÍ", contentMail);
                if (!resultGmali)
                {
                    return BadRequest("loi");
                }
                return Ok();
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }


        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginBody bodyModel)
        {
            try
            {
                User user = new User();

                user.Email = bodyModel.Email;
                user.Password = UnitCore.HashMd5( bodyModel.Password);
                var data = await userService.Login(user);
                if (data == null)
                {
                    return BadRequest("sai...");
                }
                var token = await jwtService.GenerateToken(data);

              Response.Cookies.Append("tokenjwt", token, new CookieOptions() {  SameSite = SameSiteMode.None, Secure = true });
               bool x= _redisService.SetData<string>("TokenUser:"+ token, bodyModel.Email, DateTimeOffset.Now.AddHours(7));


                return Ok(data);
            }

            catch (Exception ex)
            {
                return BadRequest("sai");

            }


        }
        [HttpPost("logout")]
        public IActionResult logout()
        {

            try
            {
                var token = HttpContext.Request.Cookies["tokenjwt"];

                _redisService.RemoveData("TokenUser:"+ token);
                Response.Cookies.Delete("tokenjwt");
            }

            catch (Exception ex)
            {
                return BadRequest();
            }

            return Ok();
        }
        [HttpPost("reset-password")]
        public async Task<IActionResult> reset_password([FromBody] ResetPasswordBody Databody)
        {

            try
            {

               
                var user = await userService.GetUserByEmail(Databody.Email);
                if (!user.Id.Any())
                {
                    return BadRequest(user);
                }
               
                var pass = UnitCore.Random();
                user.Password = UnitCore.HashMd5(pass);
                string token = await jwtService.GenerateTokenAuth(user);
                string url = string.Format(ContentMail.UrlToken, "reset-password", token);
                string contentMail = string.Format(ContentMail.ResetPassword, pass, url);

                var resultGmali = await MailUtils.SendMailGoogleSmtp("ducthangpg@gmail.com", Databody.Email, "XÁC THỰC RESET MẬT KHẨU", contentMail);
                if (!resultGmali)
                {
                    return BadRequest();
                }
                return Ok(user);

            }

            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest("sai");

            }


        }
        [HttpGet("authtoken/{TypeAuth}")]
        public async Task<IActionResult> AuthToken(string TypeAuth, string token)
        {
            try
            {
                User user = new User();
                var validateToken = jwtService.ValidateToken(token);
                user.Email = validateToken.Claims.FirstOrDefault(claim => claim.Type == "Email")?.Value;
                user.Password = validateToken.Claims.FirstOrDefault(claim => claim.Type == "PassWord")?.Value;

                if (TypeAuth == "reset-password")
                {
                    user.Id = validateToken.Claims.FirstOrDefault(claim => claim.Type == "Id")?.Value;

                    var resuit = await userService.ResetPassword(user);
                    if (resuit == null)
                    {
                        return BadRequest();
                    }
                }
                else if (TypeAuth == "registration")
                {
                    user.UserName = validateToken.Claims.FirstOrDefault(claim => claim.Type == "UserName")?.Value;

                    var resuit = await userService.Registration(user);
                    if (resuit == null)
                    {
                        return BadRequest();
                    }

                }
                return Ok("ĐÃ XÁC THỰC THÀNH CÔNG");


            }

            catch (Exception ex)
            {
                return BadRequest(ex);

            }

        }

       
    }
}
