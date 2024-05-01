using PhonebookApplication.Models;

namespace PhonebookApplication.Services.Contract
{
    public interface IContactService
    {
        IEnumerable<Contact> GetAllContacts();
        IEnumerable<Contact> GetPaginatedContacts(int page, int pageSize);
        IEnumerable<Contact> GetPaginatedContactsWithLetter(char? ch, int page, int pageSize);
        Contact? GetContactById(int contactId);
        int TotalContacts();
        int TotalContactsWithLetter(char? ch);
        string AddContact(Contact contact, IFormFile file);
        string UpdateContact(Contact contact);
        string DeleteContact(int contactId);
    }
}
