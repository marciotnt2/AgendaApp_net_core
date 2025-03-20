using AgendaApp.Application.Contacts.Commands;
using AgendaApp.Application.Contacts.Queries;
using AgendaApp.Domain.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace AgendaApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ContactsController(IMediator mediator, IUnitOfWork unitOfWork)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetContacts()
        {
            var query = new GetContactsQuery();
            var contacts = await _mediator.Send(query);
            return Ok(contacts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetContact(int id)
        {
            var query = new GetContactByIdQuery { Id = id };
            var contact = await _mediator.Send(query);
            return contact != null ? Ok(contact) : NotFound("Contact not found.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContact(int id, UpdateContactCommand command)
        {
            command.Id = id;
            var updatedContact = await _mediator.Send(command);

            return updatedContact != null ? Ok(updatedContact) : NotFound("Contact not found.");
        }

        [HttpPost]
        public async Task<IActionResult> CreateContact(CreateContactCommand comand)
        {
            var createdcontact = await _mediator.Send(comand);
            return CreatedAtAction(nameof(GetContact), new { id = createdcontact.Id }, createdcontact);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContacts(int id)
        {
            var command = new DeleteContactCommand { Id = id };
            var deletedContact = await _mediator.Send(command);

            return deletedContact != null ? Ok(deletedContact) : NotFound("Contact not found.");
        }
    }
}
