using System;
using System.Collections.Generic;
using System.Linq;
using Altkom.DIGIT_AL.dotnetCore.Basics.Models;
using Bogus;

namespace Altkom.DIGIT_AL.dotnetCore.Basics.FakeServices.Models
{
    public class CustomerFaker : BaseFaker<Customer>
    {
        public CustomerFaker() {
            RuleFor(x => x.FirstName, y => y.Name.FirstName());
            RuleFor(x => x.LastName, y => y.Name.LastName());
        }

        public static List<Customer> Generate() => new CustomerFaker().Generate(10);
    }
}
