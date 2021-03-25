using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MyContacts.API.Domain.Models;
using MyContacts.API.Domain.Services.Communication;

namespace MyContacts.API.Domain.Services
{
    public interface IContactService
    {
        Task<IEnumerable<Contact>> ListAsync(Expression<Func<Contact, bool>> filter = null);
        Task<ContactResponse> SaveAsync(Contact contact);
        Task<ContactResponse> FindByIdAsync(int id);
        Task<ContactResponse> UpdateAsync(int id, Contact contact);
        Task<ContactResponse> DeleteAsync(int id);
    }
}
