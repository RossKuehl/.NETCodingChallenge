using System.ComponentModel.DataAnnotations;

namespace MyContacts.API.Resources
{
    public class NameResource
    {
        [Required]
        public string First { get; set; }
        public string Middle { get; set; }
        [Required]
        public string Last { get; set; }
    }
}
