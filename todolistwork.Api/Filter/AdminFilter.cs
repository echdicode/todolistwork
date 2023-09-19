using CleanArch.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;
using todolistwork.Api.Authentication;
using todolistwork.Api.Controllers;

namespace todolistwork.Api.Filter
{
    public class AdminFilter: Attribute, IAuthorizationFilter
    {
        private readonly IConfiguration configuration;
        private readonly JwtService jwtService;

        public AdminFilter(IConfiguration configuration)
        {
            this.configuration = configuration;
            jwtService = new JwtService(configuration);

        }
    

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var token = context.HttpContext.Request.Cookies["tokenjwt"];
            var errorController = new ErrorController();

            if (token.Any())
            {
                ClaimsPrincipal validateToken = jwtService.ValidateToken(token, "SecretAdmin");
                if (validateToken == null)
                {
                    context.Result = errorController.NotAuthorized();
                }
                else
                {
                    var x= validateToken.Claims.FirstOrDefault(claim => claim.Type == "IsSuperuser")?.Value;
                    Console.WriteLine("0000000000000000000000000  "+x);

                    if (x == "false")
                    {
                        context.Result = errorController.NotAuthorized();

                        
                    }
                    else
                    {
                        context.Result = errorController.NotAuthorized();

                        context.HttpContext.Items["Id"] = validateToken.Claims.FirstOrDefault(claim => claim.Type == "Id")?.Value;
                        context.HttpContext.Items["Username"] = validateToken.Claims.FirstOrDefault(claim => claim.Type == "Username")?.Value;


                    }

                }


            }
            else
            {
                context.Result = errorController.NotAuthorized();

            }

        }
    }
}
