using System;
using System.Collections.Generic;

namespace Altkom.DIGIT_AL.dotnetCore.Basics.Models
{
    public class Order : Base
    {
        public Customer Customer {get; set;}
        public List<Product> Products {get; set;}
    }
}