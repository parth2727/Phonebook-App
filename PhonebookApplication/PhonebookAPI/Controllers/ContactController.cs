using Microsoft.AspNetCore.Mvc;
using PhonebookAPI.Dtos;
using PhonebookAPI.Models;
using PhonebookAPI.Services.Contract;

namespace PhonebookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;
        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet("GetAllContacts")]
        public IActionResult GetAllContacts()
        {
            var response = _contactService.GetAllContacts();
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpGet("GetContactById/{id}")]
        public IActionResult GetContactById(int id)
        {
            var response = _contactService.GetContactById(id);

            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
        [HttpPost("Create")]
        public IActionResult AddContact(AddContactDto contactDto)
        {
            if (ModelState.IsValid)
            {
                var contact = new Contact()
                {
                    FirstName = contactDto.FirstName,
                    LastName = contactDto.LastName,
                    Company = contactDto.Company,
                    Email = contactDto.Email,
                    ContactNumber = contactDto.ContactNumber,
                    //FileName = contactDto.FileName,
                };
                var result = _contactService.AddContact(contact,contactDto.File);
                return !result.Success ? BadRequest(result) : Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("ModifyContact")]
        public IActionResult UpdateContact(UpdateContactDto contactDto)
        {
            var contact = new Contact()
            {
                ContactId = contactDto.ContactId,
                FirstName = contactDto.FirstName,
                LastName = contactDto.LastName,
                Company = contactDto.Company,
                Email = contactDto.Email,
                ContactNumber = contactDto.ContactNumber,
                //FileName = contactDto.FileName,
            };
            var response = _contactService.UpdateContact(contact);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            else
            {
                return Ok(response);
            }
        }

        [HttpDelete("Remove/{id}")]
        public IActionResult RemoveContact(int id)
        {
            if (id > 0)
            {
                var response = _contactService.DeleteContact(id);
                if (!response.Success)
                {
                    return BadRequest(response);
                }
                else
                {
                    return Ok(response);
                }
            }
            else
            {
                return BadRequest("Please enter proper data.");
            }
        }
    }
}
