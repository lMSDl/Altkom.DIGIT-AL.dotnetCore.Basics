using Altkom.DIGIT_AL.dotnetCore.Basics.FakeServices.Models;
using Altkom.DIGIT_AL.dotnetCore.Basics.IServices;
using Altkom.DIGIT_AL.dotnetCore.Basics.Models;

namespace Altkom.DIGIT_AL.dotnetCore.Basics.FakeServices
{
    public class FakeOrdersService : FakeBaseService<Order>, IOrdersService
    {
        public FakeOrdersService(OrderFaker faker, int count) : base(faker, count)
        {
        }
    }
}