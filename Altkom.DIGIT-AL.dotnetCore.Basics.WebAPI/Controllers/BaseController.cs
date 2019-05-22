
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Altkom.DIGIT_AL.dotnetCore.Basics.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    //[Produces("application/xml")]
    public abstract class BaseController : ControllerBase {
        protected ILogger Logger {get;}
        protected BaseController(ILogger logger) {
                Logger = logger;
        }
    }
}