namespace MyContacts.API.Domain.Models
{
    public enum PhoneType : byte
    {
        Home,
        Work,
        Mobile
    }

    public class Phone
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public PhoneType Type { get; set; }
        public int ContactId { get; set; }
        public Contact Contact { get; set; }
    }
}
