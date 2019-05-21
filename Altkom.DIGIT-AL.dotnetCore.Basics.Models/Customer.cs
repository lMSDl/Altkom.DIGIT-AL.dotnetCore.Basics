using System;

namespace Altkom.DIGIT_AL.dotnetCore.Basics.Models
{
    public class Customer : Base
    {
        public string FirstName {get; set;}
        public string LastName {get; set;}
        public Gender Gender {get; set;}
        public Guid? LayalityCard {get; set;}

    }
}
