using Altkom.DIGIT_AL.dotnetCore.Basics.Models;
using Bogus;

namespace Altkom.DIGIT_AL.dotnetCore.Basics.FakeServices.Models
{
    public class ProductFaker : BaseFaker<Product>
    {
        public ProductFaker()
        {
            RuleFor(p => p.Name, f => f.Commerce.ProductName());
        }
    }
}