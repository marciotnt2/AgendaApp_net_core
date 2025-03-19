using AgendaApp.Domain.Abstractions;
using AgendaApp.Domain.Entities;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaApp.Application.Contacts.Commands
{
    public class CreateContactCommand : ContactCommandBase
    {
        public class CreateContactCommandHandler : IRequestHandler<CreateContactCommand, Contact>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IValidator<CreateContactCommand> _validator;
            private readonly IMediator _mediator;
            public CreateContactCommandHandler(IUnitOfWork unitOfWork,
                                              IValidator<CreateContactCommand> validator,
                                              IMediator mediator)
            {
                _unitOfWork = unitOfWork;
                _validator = validator;
                _mediator = mediator;
            }
            public async Task<Contact> Handle(CreateContactCommand request, CancellationToken cancellationToken)
            {
                _validator.ValidateAndThrow(request);

                var newContact = new Contact(request.Name, request.Email, request.Phone);

                await _unitOfWork.ContactRepository.AddContact(newContact);
                await _unitOfWork.CommitAsync();

                return newContact;
            }
        }

    }
}
