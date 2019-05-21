using System;

namespace Altkom.DIGIT_AL.dotnetCore.Basics.Models
{
    public abstract class Base
    {
        public int Id {get; set;}
        
        public bool ShouldSerializeId() => Id != default(int);
    }
}
