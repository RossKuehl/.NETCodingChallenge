using MyContacts.API.Domain.Models;

namespace MyContacts.API.Domain.Services.Communication
{
    public class ContactResponse : BaseResponse<Contact>
    {
        /// <summary>
        /// Creates a success response.
        /// </summary>
        /// <param name="contact">Saved category.</param>
        /// <returns>Response.</returns>
        public ContactResponse(Contact contact) : base(contact)
        { }

        /// <summary>
        /// Creates am error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <returns>Response.</returns>
        public ContactResponse(string message) : base(message)
        { }
    }
}
