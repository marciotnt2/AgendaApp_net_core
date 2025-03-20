using AgendaApp.Domain.Entities;
using MediatR;

namespace AgendaApp.Application.Contacts.Commands
{
    public abstract class ContactCommandBase : IRequest<Contact>
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
    }
}
