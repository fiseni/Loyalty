using PozitronDev.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerApp.Core.ValueObjects
{
    public class Address : ValueObject
    {
        public string Street { get; private set; }
        public string City { get; private set; }
        public string PostalCode { get; private set; }
        public string Country { get; private set; }

        private Address() { }

        public Address(string street, string city, string postalCode, string country)
        {
            this.Street = street.ValidateFor().NullOrEmpty();
            this.City = city.ValidateFor().NullOrEmpty();
            this.PostalCode = postalCode.ValidateFor().NullOrEmpty();
            this.Country = country.ValidateFor().NullOrEmpty();
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return this.Street;
            yield return this.City;
            yield return this.PostalCode;
            yield return this.Country;
        }
    }
}
