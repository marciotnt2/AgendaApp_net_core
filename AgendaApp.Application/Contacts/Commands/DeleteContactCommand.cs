using AgendaApp.Domain.Abstractions;
using AgendaApp.Domain.Entities;
using MediatR;

namespace AgendaApp.Application.Contacts.Commands
{
    public sealed class DeleteContactCommand : IRequest<Contact>
    {
        public int Id { get; set; }

        public class DeleteContactCommandHandler : IRequestHandler<DeleteContactCommand, Contact>
        {
            private readonly IUnitOfWork _unitOfWork;
            public DeleteContactCommandHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }
            public async Task<Contact> Handle(DeleteContactCommand request,
                         CancellationToken cancellationToken)
            {
                var deletedContact = await _unitOfWork.ContactRepository.DeleteContact(request.Id);

                if (deletedContact is null)
                    throw new InvalidOperationException("Contact not found");

                await _unitOfWork.CommitAsync();
                return deletedContact;
            }
        }
    }
}
