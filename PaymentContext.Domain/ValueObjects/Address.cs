using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flunt.Validations;
using PaymentContext.Shared.ValueObjects;

namespace PaymentContext.Domain.ValueObjects
{
    public class Address : ValueObject
    {
        public Address(string street, string number, string neighborhood, string city, string state, string country, string zipCode, string complement)
        {
            Street = street;
            Number = number;
            Neighborhood = neighborhood;
            City = city;
            State = state;
            Country = country;
            ZipCode = zipCode;
            Complement = complement;

            AddNotifications(new Contract<string>()
                    .Requires()
                    .IsGreaterOrEqualsThan(street, 3, "Address.Street", "Endereço deve conter pelo menos 3 caracteres.")
                    .IsGreaterOrEqualsThan(city, 3, "Address.City", "Cidade deve conter pelo menos 3 caracteres.")
                    .IsGreaterOrEqualsThan(state, 3, "Address.State", "Estado deve conter pelo menos 3 caracteres.")
                    .IsGreaterOrEqualsThan(country, 3, "Address.Country", "País deve conter pelo menos 3 caracteres."));
        }

        public string Street { get; private set; }
        public string Number { get; private set; }
        public string Neighborhood { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string Country { get; private set; }
        public string ZipCode { get; private set; }
        public string Complement { get; private set; }
    }
}