using PhonebookApplication.Models;

namespace PhonebookApplication.Data.Contract
{
    public interface IContactRepository
    {
        Contact? GetContact(int id);
        IEnumerable<Contact> GetAll();
        bool ContactExists(string contactNumber);
        bool ContactExists(int contactId, string contactNumber);
        bool InsertContact(Contact contact);
        bool UpdateContact(Contact contact);
        bool DeleteContact(int id);
        IEnumerable<Contact> GetPaginatedContacts(int page, int pageSize);
        IEnumerable<Contact> GetPaginatedContactWithLetter(char? ch, int page, int pageSize);
        int TotalContactWithLetter(char? ch);
        int TotalContacts();
    }
}
