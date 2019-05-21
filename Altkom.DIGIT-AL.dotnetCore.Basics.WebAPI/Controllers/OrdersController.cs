using System.Threading.Tasks;
using Altkom.DIGIT_AL.dotnetCore.Basics.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Altkom.DIGIT_AL.dotnetCore.Basics.WebAPI.Controllers
{
    public class OrdersController : BaseController
    {
        private IOrdersService _ordersService;
        public OrdersController(ILogger<OrdersController> logger, IOrdersService ordersService) : base(logger) {
            _ordersService = ordersService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _ordersService.GetAsync());
        }

    }
}