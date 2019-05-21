using System;
using Altkom.DIGIT_AL.dotnetCore.Basics.Models;
using Bogus;

namespace Altkom.DIGIT_AL.dotnetCore.Basics.FakeServices.Models
{
    public abstract class BaseFaker<T> : Faker<T> where T : Base
    {
        protected BaseFaker() : base("pl")
        {
            StrictMode(true);
            RuleFor(x => x.Id, y => y.IndexFaker);
        }
    }
}
