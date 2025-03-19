using AgendaApp.Domain.Abstractions;
using AgendaApp.Domain.Entities;
using AgendaApp.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace AgendaApp.Infrastructure.Repositories
{
    public class ContactRepository : IContactRepository
    {
        protected readonly AppDbContext db;

        public ContactRepository(AppDbContext _db)
        {
            this.db = _db;
        }

        public async Task<Contact> AddContact(Contact contact)
        {
            if (contact == null) throw new ArgumentNullException(nameof(contact));
                await db.Contacts.AddAsync(contact);
            return contact;
        }

        public async Task<Contact> DeleteContact(int id)
        {
            var contact = await GetContactById(id);

            if (contact == null)
                throw new InvalidOperationException("Contact not found");

            db.Contacts.Remove(contact);
            return contact;
        }

        public async Task<Contact> GetContactById(int id)
        {
            var contact = await db.Contacts.FindAsync(id);

            if (contact == null)
                throw new InvalidOperationException("Contact not found");

            return contact;
        }

        public async Task<IEnumerable<Contact>> GetContacts()
        {
            var contacts = await db.Contacts.ToListAsync();
            return contacts ?? Enumerable.Empty<Contact>();
        }

        public void UpdateContact(Contact contact)
        {
            if (contact == null)
                throw new InvalidOperationException("Contact not found");
            db.Contacts.Update(contact);
            
        }
    }
}
