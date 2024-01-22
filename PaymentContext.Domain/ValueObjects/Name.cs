using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flunt.Validations;
using PaymentContext.Shared.ValueObjects;

namespace PaymentContext.Domain.ValueObjects
{
    public class Name : ValueObject
    {
        public Name(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;

            AddNotifications(new Contract<string>()
                                .Requires()
                                .IsGreaterOrEqualsThan(FirstName, 3, "Name.FirstName", "Nome deve conter pelo menos 3 caracteres.")
                                .IsGreaterOrEqualsThan(LastName, 3, "Name.LastName", "Sobrenome deve conter pelo menos 3 caracteres.")
                                .IsLowerOrEqualsThan(FirstName, 40, "Name.FistName", "Nome deve conter no máximo até 40 caracteres"));
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }
}