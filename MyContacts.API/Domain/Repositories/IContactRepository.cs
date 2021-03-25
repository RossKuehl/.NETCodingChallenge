using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MyContacts.API.Domain.Models;

namespace MyContacts.API.Domain.Repositories
{
    public interface IContactRepository
    {
        Task<IEnumerable<Contact>> ListAsync(Expression<Func<Contact, bool>> filter = null);
        Task AddAsync(Contact contact);
        Task<Contact> FindByIdAsync(int id);
        void Update(Contact oldContact, Contact contact);
        void Remove(Contact contact);
    }
}
