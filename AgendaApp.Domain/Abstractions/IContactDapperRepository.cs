using AgendaApp.Domain.Entities;

namespace AgendaApp.Domain.Abstractions
{
    public interface IContactDapperRepository
    {
        Task<IEnumerable<Contact>> GetContacts();
        Task<Contact> GetContactById(int id);
    }
}
