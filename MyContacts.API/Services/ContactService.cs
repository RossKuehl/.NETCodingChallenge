using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using MyContacts.API.Domain.Models;
using MyContacts.API.Domain.Repositories;
using MyContacts.API.Domain.Services;
using MyContacts.API.Domain.Services.Communication;

namespace MyContacts.API.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemoryCache _cache;

        public ContactService(IContactRepository contactRepository, IUnitOfWork unitOfWork, IMemoryCache cache)
        {
            _contactRepository = contactRepository;
            _unitOfWork = unitOfWork;
            _cache = cache;
        }

        public async Task<ContactResponse> DeleteAsync(int id)
        {
            var existingContact = await _contactRepository.FindByIdAsync(id);

            if (existingContact == null)
                return new ContactResponse("Contact not found.");

            try
            {
                _contactRepository.Remove(existingContact);
                await _unitOfWork.CompleteAsync();

                _cache.Remove($"contact-{id}");
                _cache.Remove("contacts-list");

                return new ContactResponse(existingContact);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new ContactResponse($"An error occurred when deleting the contact: {ex.Message}");
            }
        }

        public async Task<ContactResponse> FindByIdAsync(int id)
        {
            var existingContact = await _cache.GetOrCreateAsync($"contact-{id}", (entry) =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1);
                return _contactRepository.FindByIdAsync(id);
            });

            if (existingContact == null)
                return new ContactResponse("Contact not found.");

            return new ContactResponse(existingContact);
        }

        public async Task<IEnumerable<Contact>> ListAsync(Expression<Func<Contact, bool>> filter = null)
        {
            var contacts = await _cache.GetOrCreateAsync("contacts-list", (entry) => {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1);
                return _contactRepository.ListAsync();
            });

            return contacts;
        }

        public async Task<ContactResponse> SaveAsync(Contact contact)
        {
            try
            {
                await _contactRepository.AddAsync(contact);
                await _unitOfWork.CompleteAsync();

                _cache.Remove("contacts-list");

                return new ContactResponse(contact);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new ContactResponse($"An error occurred when saving the contact: {ex.Message}");
            }
        }

        public async Task<ContactResponse> UpdateAsync(int id, Contact contact)
        {
            var existingContact = await _contactRepository.FindByIdAsync(id);

            if (existingContact == null)
                return new ContactResponse("Contact not found.");

            try
            {
                contact.Id = existingContact.Id;
                _contactRepository.Update(existingContact, contact);
                await _unitOfWork.CompleteAsync();

                _cache.Remove($"contact-{id}");
                _cache.Remove("contacts-list");

                return new ContactResponse(existingContact);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new ContactResponse($"An error occurred when updating the contact: {ex.Message}");
            }
        }
    }
}
