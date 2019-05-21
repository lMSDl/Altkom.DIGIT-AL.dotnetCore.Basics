using Altkom.DIGIT_AL.dotnetCore.Basics.IServices;
using Altkom.DIGIT_AL.dotnetCore.Basics.Models;
using Bogus;
using System.Linq;

namespace Altkom.DIGIT_AL.dotnetCore.Basics.FakeServices.Models
{
    public class OrderFaker : BaseFaker<Order>
    {
        public OrderFaker(ICustomersService fakeCustomersService, IProductsService fakeProductsService)
        {
            RuleFor(p => p.Customer, f => f.PickRandom(fakeCustomersService.GetAsync().Result));
            RuleFor(p => p.Products, f => f.PickRandom(fakeProductsService.GetAsync().Result, f.Random.Number(1, 10)).ToList());
        }
    }
}