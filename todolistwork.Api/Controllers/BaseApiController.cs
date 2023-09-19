using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using todolistwork.Api.Filter;

namespace todolistwork.Api.Controllers
{
    [TypeFilter(typeof(UserFilter))]

    [ApiController]
    public class BaseApiController : ControllerBase
    {
    }
}