using AgendaApp.Domain.Abstractions;
using AgendaApp.Domain.Entities;
using MediatR;

namespace AgendaApp.Application.Contacts.Commands
{
    public sealed class UpdateContactCommand : ContactCommandBase
    {
        public int Id { get; set; }

        public class UpdateContactCommandHandler :
                IRequestHandler<UpdateContactCommand, Contact>
        {
            private readonly IUnitOfWork _unitOfWork;
            public UpdateContactCommandHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }
            public async Task<Contact> Handle(UpdateContactCommand request, CancellationToken cancellationToken)
            {
                var existingContact = await _unitOfWork.ContactRepository.GetContactById(request.Id);

                if (existingContact is null)
                    throw new InvalidOperationException("Contact not found");

                existingContact.Update(request.Name, request.Email, request.Phone);
                _unitOfWork.ContactRepository.UpdateContact(existingContact);
                await _unitOfWork.CommitAsync();

                return existingContact;
            }
        }
    }
}
