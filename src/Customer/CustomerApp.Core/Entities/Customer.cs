using CustomerApp.Core.Enumerations;
using CustomerApp.Core.ValueObjects;
using Loyalty.SharedKernel;
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
    public class Customer : AuditableEntity, IAggregateRoot
    {
        public Guid Id { get; private set; }
        public CustomerTypeEnum Type { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }

        // Instead of having dozens of properties, try to create logical groups.
        // These can be value objects, or even you may have 1:1 relationships.
        public Address Address { get; private set; }


        private readonly List<Contact> _contacts = new List<Contact>();
        public IEnumerable<Contact> Contacts => _contacts.AsEnumerable();


        // Required by EF.
        private Customer() { }

        // Required only for the ADO.NET approach.
        public Customer(Guid id, CustomerTypeEnum type, string name, string email, Address address)
            : this(type, name, email, address)
        {
            this.Id = id; 
        }

        public Customer(CustomerTypeEnum type, string name, string email, Address address)
        {
            this.Type = type;
            this.Name = name.ValidateFor().NullOrEmpty();
            this.Email = email.ValidateFor().NullOrEmpty();

            UpdateAddress(address);
        }

        /// It's important not to update the Address if there are no changes.
        /// If updated, since it's a new object, EF tracker will mark it as New.
        /// This can have implications on "Auditing" logic, if you have implemented one. 
        public void UpdateAddress(Address address)
        {
            address.ValidateFor().Null();

            if (this.Address is null || !this.Address.Equals(address))
            {
                this.Address = address;
            }
        }

        public Contact AddContact(Contact contact)
        {
            contact.ValidateFor().Null();

            _contacts.Add(contact);

            return contact;
        }

        public Contact AddContact(Person person)
        {
            person.ValidateFor().Null();

            return AddContact(new Contact(person));
        }

        public void DeleteContact(Guid contactId)
        {
            var contact = Contacts.FirstOrDefault(x => x.Id == contactId);
            contact.ValidateFor().NotFound(contactId);

            _contacts.Remove(contact);
        }
    }
}
