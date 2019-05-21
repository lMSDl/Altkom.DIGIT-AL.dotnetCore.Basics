
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Altkom.DIGIT_AL.dotnetCore.Basics.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseController : ControllerBase {
        protected ILogger Logger {get;}
        protected BaseController(ILogger logger) {
                Logger = logger;
        }
    }
}