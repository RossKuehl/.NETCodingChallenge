using System.Collections.Generic;

namespace MyContacts.API.Domain.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public List<Phone> Phone { get; set; } = new List<Phone>();
        public string Email { get; set; }
    }
}
