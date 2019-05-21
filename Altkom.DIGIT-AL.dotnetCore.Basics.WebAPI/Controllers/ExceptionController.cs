using System;
using Altkom.DIGIT_AL.dotnetCore.Basics.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Altkom.DIGIT_AL.dotnetCore.Basics.WebAPI.Controllers
{
    public class ExceptionController : BaseController
    {
        public ExceptionController(ILogger<ExceptionController> logger) : base(logger)
        {
        }

        [HttpGet]
        public IActionResult Get()
        {
            throw new Exception("Something wrong");
        }
    }
}