using AgendaApp.Domain.Abstractions;
using AgendaApp.Domain.Entities;
using Dapper;
using System.Data;


namespace AgendaApp.Infrastructure.Repositories
{
    public class ContactDapperRepository : IContactDapperRepository
    {
        private readonly IDbConnection _dbConnection;

        public ContactDapperRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public async Task<Contact> GetContactById(int id)
        {
            string query = "SELECT * FROM Contacts WHERE Id = @Id";
            return await _dbConnection.QueryFirstOrDefaultAsync<Contact>(query, new { Id = id });
        }

        public async Task<IEnumerable<Contact>> GetContacts()
        {
            string query = "SELECT * FROM Contacts";
            return await _dbConnection.QueryAsync<Contact>(query);
        }

        public async Task<IEnumerable<Contact>> GetContactByEmail(string email)
        {
            string query = "SELECT * FROM Contacts WHERE email = @email";
            return await _dbConnection.QueryAsync<Contact>(query);
        }
    }
}
