using System.Threading.Tasks;
using Altkom.DIGIT_AL.dotnetCore.Basics.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Altkom.DIGIT_AL.dotnetCore.Basics.WebAPI.Controllers
{
    public class ProductsController : BaseController
    {
        private IProductsService _productsService;
        public ProductsController(ILogger<ProductsController> logger, IProductsService productsService) : base(logger) {
            _productsService = productsService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _productsService.GetAsync());
        }

    }
}