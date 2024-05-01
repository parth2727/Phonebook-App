using PhonebookApplication.Data.Contract;
using PhonebookApplication.Models;
using PhonebookApplication.Services.Contract;

namespace PhonebookApplication.Services.Implementation
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;
        public ContactService(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }
        public IEnumerable<Contact> GetAllContacts()
        {
            var contacts = _contactRepository.GetAll();
            if (contacts != null && contacts.Any())
            {
                foreach (var contact in contacts.Where(c => c.FileName == string.Empty))
                {
                    contact.FileName = "default-image.png";
                }

                return contacts;
            }

            return new List<Contact>();
        }

        public IEnumerable<Contact> GetPaginatedContacts(int page, int pageSize)
        {
            var contacts = _contactRepository.GetPaginatedContacts(page, pageSize);
            if (contacts != null && contacts.Any())
            {
                foreach (var contact in contacts.Where(c => c.FileName == string.Empty))
                {
                    contact.FileName = "default-image.png";
                }

                return contacts;
            }

            return new List<Contact>();
        }
        public IEnumerable<Contact> GetPaginatedContactsWithLetter(char? ch ,int page, int pageSize)
        {
            var contacts = _contactRepository.GetPaginatedContactWithLetter(ch,page, pageSize);
            if (contacts != null && contacts.Any())
            {
                foreach (var contact in contacts.Where(c => c.FileName == string.Empty))
                {
                    contact.FileName = "default-image.png";
                }

                return contacts;
            }

            return new List<Contact>();
        }

        public Contact? GetContactById(int contactId)
        {
            var contact = _contactRepository.GetContact(contactId);
            return contact;
        }

        public int TotalContacts()
        {
            return _contactRepository.TotalContacts();
        }
        public int TotalContactsWithLetter(char? ch)
        {
            return _contactRepository.TotalContactWithLetter(ch);
        }

        public string AddContact(Contact contact, IFormFile file)
        {
            if (_contactRepository.ContactExists(contact.ContactNumber))
            {
                return "Contact number already exists.";
            }
            var fileName = string.Empty;
            if (file != null && file.Length > 0)
            {
                // Process the uploaded file(eq. save it to disk)
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", file.FileName);

                // Save the file to storage and set path
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                    fileName = file.FileName;
                }
                contact.FileName = fileName;
            }
            var result = _contactRepository.InsertContact(contact);
            return result ? "Contact saved successfully." : "Something went wrong, please try after some time.";
        }

        public string UpdateContact(Contact contact)
        {
            var message = string.Empty;
            if (_contactRepository.ContactExists(contact.ContactId, contact.ContactNumber))
            {
                message = "Contact number already exists.";
                return message;
            }

            var existingContact = _contactRepository.GetContact(contact.ContactId);

            var result = false;
            if (existingContact != null)
            {
                existingContact.FirstName = contact.FirstName;
                existingContact.LastName = contact.LastName;
                existingContact.ContactNumber = contact.ContactNumber;
                existingContact.Email = contact.Email;
                existingContact.Company = contact.Company;
                /*existingContact.FileName = contact.FileName;
                existingContact.FileName = "default-image.png";*/
                result = _contactRepository.UpdateContact(existingContact);
            }

            message = result ? "Contact updated successfully." : "Something went wrong, please try after some time.";

            return message;
        }

        public string DeleteContact(int contactId)
        {
            var result = _contactRepository.DeleteContact(contactId);
            if (result)
            {
                return "Contact deleted successfully.";
            }
            else
            {
                return "Something went wrong, please try after some time.";
            }
        }
    }
}
