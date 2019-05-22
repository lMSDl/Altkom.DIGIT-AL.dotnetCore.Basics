using System;
using System.ComponentModel;

namespace Altkom.DIGIT_AL.dotnetCore.Basics.Models
{
    public class Customer : Base
    {
        [DisplayName("Imię")]
        public string FirstName {get; set;}
        [DisplayName("Nazwisko")]
        public string LastName {get; set;}
        [DisplayName("Płeć")]
        public Gender Gender {get; set;}
        [DisplayName("Karta lojalnościowa")]
        public Guid? LoyalityCard {get; set;}

    }
}
