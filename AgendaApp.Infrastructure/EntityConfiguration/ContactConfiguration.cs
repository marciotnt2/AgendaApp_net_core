using AgendaApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgendaApp.Infrastructure.EntityConfiguration
{
    internal class ContactConfiguration : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Name).HasMaxLength(50).IsRequired();
            builder.Property(m => m.Email).HasMaxLength(50).IsRequired();
            builder.Property(m => m.Phone).HasMaxLength(50).IsRequired();

            builder.HasData(
                new Contact(1,"Marcio Galdino", "marcio@email.com", "81995208340")
            );
        }
    }
}
