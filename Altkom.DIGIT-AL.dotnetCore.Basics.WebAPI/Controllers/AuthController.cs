
using System.Threading.Tasks;
using Altkom.DIGIT_AL.dotnetCore.Basics.IServices;
using Altkom.DIGIT_AL.dotnetCore.Basics.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Altkom.DIGIT_AL.dotnetCore.Basics.WebAPI.Controllers
{
    public class AuthController : BaseController {

        private IAuthService _authService;

        public AuthController(IAuthService authService, ILogger<AuthController> logger) : base(logger) {
            _authService = authService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post(User user) {
            var token = await _authService.Authenticate(user);

            if(token == null)
                return BadRequest(new {message = "Incorrect credentials"});
             return Ok(token);
        }
    }
}