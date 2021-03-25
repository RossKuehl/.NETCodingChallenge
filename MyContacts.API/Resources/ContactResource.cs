using System.Collections.Generic;

namespace MyContacts.API.Resources
{
    public class ContactResource
    {
        public int Id { get; set; }
        public NameResource Name { get; set; }
        public AddressResource Address { get; set; }
        public IEnumerable<PhoneResource> Phone { get; set; }
        public string Email { get; set; }
    }
}
