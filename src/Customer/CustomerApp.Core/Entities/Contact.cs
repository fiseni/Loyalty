using CustomerApp.Core.ValueObjects;
using PozitronDev.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerApp.Core.Entities
{
    // Here it is shown how you can encapsulate your entities. This works quite well with EntityFramework.
    // You can even define most of the properties as fully immutable (getters only), but not to confuse the team too much I'm keeping it simple, and define setters too.
    // Here is how you can create entities with immutable properties or entirely immutable entities https://fiseni.com/posts/immutable-entities-in-ef-core/
    public class Contact
    {
        public Guid Id { get; private set; }
        public Person Details { get; private set; }

        public Guid CustomerId { get; private set; }

        // Required by EF.
        private Contact() { }

        // Required only for the ADO.NET approach.
        public Contact(Guid id, Person details, Guid customerId)
            : this(details)
        {
            this.Id = id;
            this.CustomerId = customerId;
        }

        public Contact(Person details)
        {
            this.Details = details.ValidateFor().Null();
        }
    }
}
