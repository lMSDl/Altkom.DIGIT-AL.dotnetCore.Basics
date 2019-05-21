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
            RuleFor(x => x.Gender, y => y.PickRandom<Gender>());
            RuleFor(x => x.FirstName, (y, x) => y.Name.FirstName((Bogus.DataSets.Name.Gender)(x.Gender)));
            RuleFor(x => x.LastName, (y, x) => y.Name.LastName((Bogus.DataSets.Name.Gender)(x.Gender)));
            RuleFor(x => x.LayalityCard, y => y.Random.Bool() ? Guid.NewGuid() : (Guid?)null);
        }

        public static List<Customer> Generate() => new CustomerFaker().Generate(10);
    }
}
