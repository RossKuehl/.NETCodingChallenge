using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyContacts.API.Domain.Models;
using MyContacts.API.Domain.Repositories;
using MyContacts.API.Persistence.Contexts;

namespace MyContacts.API.Persistence.Repositories
{
    public class ContactRepository : BaseRepository, IContactRepository
    {
        public ContactRepository(AppDbContext context) : base(context) { }

        public async Task AddAsync(Contact contact)
        {
            await _context.Contacts.AddAsync(contact);
        }

        public async Task<Contact> FindByIdAsync(int id)
        {
            return await _context.Contacts.FindAsync(id);
        }

        public async Task<IEnumerable<Contact>> ListAsync(Expression<Func<Contact, bool>> filter = null)
        {
            return await _context.Contacts.Include(x => x.Phone).Where(filter ?? (x => true)).ToListAsync();
        }

        public void Remove(Contact contact)
        {
            _context.Contacts.Remove(contact);
        }

        public void Update(Contact oldContact, Contact contact)
        {
            _context.Entry(oldContact).CurrentValues.SetValues(contact);
        }
    }
}
