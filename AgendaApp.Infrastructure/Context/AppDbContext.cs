using AgendaApp.Domain.Entities;
using AgendaApp.Infrastructure.EntityConfiguration;
using Microsoft.EntityFrameworkCore;

namespace AgendaApp.Infrastructure.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {
        } 

        public DbSet<Contact> Contacts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ContactConfiguration());
        }
    }
}
