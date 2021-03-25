using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyContacts.API.Resources
{
    public class SaveContactResource
    {
        [Required]
        public NameResource Name { get; set; }
        public AddressResource Address { get; set; }
        public List<PhoneResource> Phone { get; set; } = new List<PhoneResource>();
        [Required]
        public string Email { get; set; }
    }
}
