namespace Altkom.DIGIT_AL.dotnetCore.Basics.Program.Models
{
    public class Person {

        public string FirstName {get; set;}
        public string LastName {get; set;}

        public string FullName => $"{FirstName} {LastName}";

    }
}