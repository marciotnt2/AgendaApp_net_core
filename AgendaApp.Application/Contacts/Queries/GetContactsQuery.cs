using AgendaApp.Domain.Abstractions;
using AgendaApp.Domain.Entities;
using MediatR;

namespace AgendaApp.Application.Contacts.Queries
{
    public class GetContactsQuery : IRequest<IEnumerable<Contact>>
    {
        public class GetContactsQueryHandler : IRequestHandler<GetContactsQuery, IEnumerable<Contact>>
        {
            private readonly IContactDapperRepository _contactDapperRepository;

            public GetContactsQueryHandler(IContactDapperRepository contactDapperRepository)
            {
                _contactDapperRepository = contactDapperRepository;
            }
            public async Task<IEnumerable<Contact>> Handle(GetContactsQuery request, CancellationToken cancellationToken)
            {
                var contacts = await _contactDapperRepository.GetContacts();
                return contacts;
            }
        }
    }
}
