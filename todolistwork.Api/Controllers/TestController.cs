using Microsoft.AspNetCore.Mvc;
using todolistwork.Application.Repository;
using todolistwork.Core.Entities;
using todolistwork.Api.Authentication;

namespace todolistwork.Api.Controllers
{
    
    [Route("api/thang")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly  JwtService jwtService;
        public TestController(IUnitOfWork unitOfWork, IConfiguration _configuration)

        {
            this._unitOfWork = unitOfWork;
            this.jwtService = new JwtService(_configuration);
        }
     

    }
}