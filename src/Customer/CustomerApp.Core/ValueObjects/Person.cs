using PozitronDev.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerApp.Core.ValueObjects
{
    public class Person : ValueObject
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string Phone { get; private set; }

        public string FullName => $"{this.FirstName} {this.LastName}";

        private Person() { }

        public Person(string firstName, string lastName, string email, string phone)
        {
            this.FirstName = firstName.ValidateFor().NullOrEmpty();
            this.LastName = lastName.ValidateFor().NullOrEmpty();
            this.Email = email.ValidateFor().NullOrEmpty();
            this.Phone = phone;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return this.FirstName;
            yield return this.LastName;
            yield return this.Email;
            yield return this.Phone;
        }
    }
}
