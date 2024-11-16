using ETicaretServer.Application.Configurations;
using ETicaretServer.Application.CustomAttributes;
using ETicaretServer.Application.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace ETicaretServer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class ApplicationServicesController : ControllerBase
    {
        readonly IApplicationService _applicationService;

        public ApplicationServicesController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }
        [HttpGet]
        [AuthorizeDefinition(ActionType = ActionType.Reading, Definition = "Get Authorize Definition Endpoints", Menu = "Application Services")]
        public IActionResult GetAuthorizeDefinitionEndPoints()
        {
            var datas =  _applicationService.GetAuthorizeDefinitionEndPoints(typeof(Program));
            return Ok(datas);
        }
    }
}
