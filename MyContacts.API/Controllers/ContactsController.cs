using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyContacts.API.Domain.Models;
using MyContacts.API.Domain.Services;
using MyContacts.API.Resources;

namespace MyContacts.API.Controllers
{
    [Route("/api/contacts")]
    [Produces("application/json")]
    [ApiController]
    public class ContactsController : Controller
    {
        private readonly IContactService _contactService;
        private readonly IMapper _mapper;

        public ContactsController(IContactService contactService, IMapper mapper)
        {
            _contactService = contactService;
            _mapper = mapper;
        }

        /// <summary>
        /// Lists all contacts.
        /// </summary>
        /// <returns>List of contacts.</returns>
        [HttpGet]
        public async Task<IEnumerable<ContactResource>> GetAllAsync()
        {
            var contacts = await _contactService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Contact>, IEnumerable<ContactResource>>(contacts);

            return resources;
        }

        /// <summary>
        /// Finds an existing contact by id.
        /// </summary>
        /// <param name="id">Contact identifier.</param>
        /// <returns>Response for the request.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ContactResource), 200)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> FindByIdAsync(int id)
        {
            var result = await _contactService.FindByIdAsync(id);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }

            var contactResource = _mapper.Map<Contact, ContactResource>(result.Resource);
            return Ok(contactResource);
        }

        /// <summary>
        /// Saves a new contact.
        /// </summary>
        /// <param name="resource">Contact data.</param>
        /// <returns>Response for the request.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(ContactResource), 201)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> PostAsync([FromBody] SaveContactResource resource)
        {
            var contact = _mapper.Map<SaveContactResource, Contact>(resource);
            var result = await _contactService.SaveAsync(contact);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }

            var contactResource = _mapper.Map<Contact, ContactResource>(result.Resource);
            return Ok(contactResource);
        }

        /// <summary>
        /// Updates an existing contact according to an identifier.
        /// </summary>
        /// <param name="id">Contact identifier.</param>
        /// <param name="resource">Updated contact data.</param>
        /// <returns>Response for the request.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ContactResource), 200)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveContactResource resource)
        {
            var contact = _mapper.Map<SaveContactResource, Contact>(resource);
            var result = await _contactService.UpdateAsync(id, contact);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }

            var contactResource = _mapper.Map<Contact, ContactResource>(result.Resource);
            return Ok(contactResource);
        }

        /// <summary>
        /// Deletes a given contact according to an identifier.
        /// </summary>
        /// <param name="id">Contact identifier.</param>
        /// <returns>Response for the request.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ContactResource), 200)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _contactService.DeleteAsync(id);

            if (!result.Success)
            {
                return BadRequest(new ErrorResource(result.Message));
            }

            var contactResource = _mapper.Map<Contact, ContactResource>(result.Resource);
            return Ok(contactResource);
        }
    }
}
