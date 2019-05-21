using Altkom.DIGIT_AL.dotnetCore.Basics.IServices;
using Altkom.DIGIT_AL.dotnetCore.Basics.Models;
using Bogus;

namespace Altkom.DIGIT_AL.dotnetCore.Basics.FakeServices
{
    public class FakeCustomersService : FakeBaseService<Customer>, ICustomersService
    {
        public FakeCustomersService(Faker<Customer> faker, int count) : base(faker, count)
        {
        }
    }
}