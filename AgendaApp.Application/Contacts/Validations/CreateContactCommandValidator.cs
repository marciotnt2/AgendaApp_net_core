using AgendaApp.Application.Contacts.Commands;
using FluentValidation;

namespace AgendaApp.Application.Contacts.Validations
{
    public class CreateContactCommandValidator : AbstractValidator<CreateContactCommand>
    {
        public CreateContactCommandValidator()
        {
            RuleFor(c => c.Name)
              .NotEmpty().WithMessage("Verifique se o nome foi informado")
              .Length(4, 50).WithMessage("O nome deve possuir de 4 a 50 caracteres");

            RuleFor(c => c.Phone)
               .NotEmpty().WithMessage("Verifique se o telefone foi informado")
               .Length(6, 50).WithMessage("O telefone de possuir de 6 a 50 caracteres");

            RuleFor(c => c.Email)
               .NotEmpty()
               .EmailAddress();

        }
    }
}
