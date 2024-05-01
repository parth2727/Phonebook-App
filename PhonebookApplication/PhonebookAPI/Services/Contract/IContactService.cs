using PhonebookAPI.Dtos;
using PhonebookAPI.Models;

namespace PhonebookAPI.Services.Contract
{
    public interface IContactService
    {
        ServiceResponse<IEnumerable<ContactDto>> GetAllContacts();
        ServiceResponse<ContactDto?> GetContactById(int contactId);
        int TotalContacts();
        ServiceResponse<string> AddContact(Contact contact, IFormFile file);
        ServiceResponse<string> UpdateContact(Contact contact);
        ServiceResponse<string> DeleteContact(int contactId);
    }
}
