using PhonebookApplication.Data.Contract;
using PhonebookApplication.Models;

namespace PhonebookApplication.Data.Implementation
{
    public class ContactRepository : IContactRepository
    {
        public readonly AppDbContext _appDbContext;
        public ContactRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public Contact? GetContact(int id)
        {
            var contact = _appDbContext.Contacts.FirstOrDefault(c => c.ContactId == id);
            return contact;
        }
        public IEnumerable<Contact> GetAll()
        {
            List<Contact> contacts = _appDbContext.Contacts.ToList();
            return contacts;
        }
        public bool InsertContact(Contact contact)
        {
            bool result = false;
            if (contact != null)
            {
                _appDbContext.Contacts.Add(contact);
                _appDbContext.SaveChanges();
                result = true;
            }
            return result;
        }
        public bool UpdateContact(Contact contact)
        {
            var result = false;
            if (contact != null)
            {
                _appDbContext.Contacts.Update(contact);
                //_appDbContext.Entry(category).State = EntityState.Modified;
                _appDbContext.SaveChanges();
                result = true;
            }
            return result;
        }
        public bool DeleteContact(int id)
        {
            var result = false;
            var contact = _appDbContext.Contacts.Find(id);
            if (contact != null)
            {
                _appDbContext.Contacts.Remove(contact);
                _appDbContext.SaveChanges();
                result = true;
            }

            return result;
        }
        public bool ContactExists(string contactNumber)
        {
            var contact = _appDbContext.Contacts.FirstOrDefault(c => c.ContactNumber == contactNumber);
            if (contact != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool ContactExists(int contactId, string contactNumber)
        {
            var contact = _appDbContext.Contacts.FirstOrDefault(c => c.ContactId != contactId && c.ContactNumber == contactNumber);
            if (contact != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public IEnumerable<Contact> GetPaginatedContacts(int page, int pageSize)
        {
            int skip = (page - 1) * pageSize;
            return _appDbContext.Contacts
                .OrderBy(c => c.ContactId)
                .Skip(skip)
                .Take(pageSize)
                .ToList();
        }

        public IEnumerable<Contact> GetPaginatedContactWithLetter(char? ch,int page, int pageSize)
        {
            int skip = (page - 1) * pageSize;
            return _appDbContext.Contacts.Where(c => c.FirstName.StartsWith(ch.ToString().ToLower()))
                .OrderBy(c => c.ContactId)
                .Skip(skip)
                .Take(pageSize)
                .ToList();
        }
        public int TotalContacts()
        {
            return _appDbContext.Contacts.Count();
        }

        public int TotalContactWithLetter(char? ch)
        {
            return _appDbContext.Contacts.Where(c => c.FirstName.StartsWith(ch.ToString().ToLower())).Count();
        }

    }
}
