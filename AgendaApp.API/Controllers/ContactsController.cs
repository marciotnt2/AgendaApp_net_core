using AgendaApp.Domain.Abstractions;
using AgendaApp.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgendaApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ContactsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetContacts()
        {
            var contacts = await _unitOfWork.ContactRepository.GetContacts();
            return Ok(contacts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetContact(int id)
        {
            var contact = await _unitOfWork.ContactRepository.GetContactById(id);
            if (contact == null)
            {
                return NotFound("contact not found.");
            }
            return Ok(contact);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContact(int id, Contact contact)
        {
            var existingContact = await _unitOfWork.ContactRepository.GetContactById(id);
            if (existingContact == null || contact == null)
            {
                return NotFound("contact not found.");
            }

            existingContact.Update(name: contact.Name, contact.Email, contact.Phone);
            _unitOfWork.ContactRepository.UpdateContact(existingContact);
            await _unitOfWork.CommitAsync();
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> CreateContact(Contact contact)
        {
           
            if (contact == null)
            {
                return NotFound("contact not found.");
            }
            var contactCreated = await _unitOfWork.ContactRepository.AddContact(contact);
            await _unitOfWork.CommitAsync();
            return CreatedAtAction(nameof(GetContact), new { id = contactCreated.Id }, contactCreated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContacts(int id)
        {
            var deletedContact = await _unitOfWork.ContactRepository.DeleteContact(id);

            if(deletedContact == null)
            {
                return NotFound("contact not found.");
            }
            await _unitOfWork.CommitAsync();
            return Ok(deletedContact);
        }
    }
}
