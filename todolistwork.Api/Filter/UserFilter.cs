using CleanArch.Api.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;
using todolistwork.Api.Authentication;
using todolistwork.Api.Controllers;
using todolistwork.Application.ICache;

namespace todolistwork.Api.Filter
{
    public class UserFilter : Attribute, IAuthorizationFilter
    {
        private readonly IConfiguration configuration;
        private readonly JwtService jwtService;
        private readonly IRedisService _redisService;


        public UserFilter(IConfiguration configuration, IRedisService _redisService)
        {
            this.configuration = configuration;
            jwtService = new JwtService(configuration);
            this._redisService = _redisService;

        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            
            var token = context.HttpContext.Request.Cookies["tokenjwt"];
            var authController = new ErrorController();
            string x= _redisService.GetData<string>("TokenUser:" + token);
            if (token != null && x != default)
            {
                ClaimsPrincipal validateToken = jwtService.ValidateToken(token);
                if (validateToken == null)
                {
                    context.Result = authController.NotAuthorized();
                }
                else
                {
                    context.HttpContext.Items["Id"] = validateToken.Claims.FirstOrDefault(claim => claim.Type == "Id")?.Value;
                    context.HttpContext.Items["Email"] = validateToken.Claims.FirstOrDefault(claim => claim.Type == "Email")?.Value;
                }
               

            }
            else
            {
                context.Result = authController.NotAuthorized();

            }

        }

     
    }
}
