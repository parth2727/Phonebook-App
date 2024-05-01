using PhonebookAPI.Data.Contract;
using PhonebookAPI.Dtos;
using PhonebookAPI.Models;
using PhonebookAPI.Services.Contract;

namespace PhonebookAPI.Services.Implementation
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;
        public ContactService(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }
        public ServiceResponse<IEnumerable<ContactDto>> GetAllContacts()
        {
            var response = new ServiceResponse<IEnumerable<ContactDto>>();

            var contacts = _contactRepository.GetAll();

            if (contacts == null)
            {
                response.Success = false;
                response.Data = new List<ContactDto>();
                response.Message = "No record found.";
                return response;
            }

            List<ContactDto> contactDtos = new List<ContactDto>();
            foreach (var contact in contacts.ToList())
            {
                contactDtos.Add(
                    new ContactDto()
                    {
                        ContactId = contact.ContactId,
                        FirstName = contact.FirstName,
                        LastName = contact.LastName,
                        Company = contact.Company,
                        Email = contact.Email,
                        ContactNumber = contact.ContactNumber,
                        FileName = contact.FileName,
                    });
            }
            response.Data = contactDtos;
            return response;
        }
        public ServiceResponse<ContactDto?> GetContactById(int contactId)
        {
            var response = new ServiceResponse<ContactDto>();
            var contact = _contactRepository.GetContact(contactId);
            var contactDto = new ContactDto()
            {
                ContactId = contact.ContactId,
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                Company = contact.Company,
                Email = contact.Email,
                ContactNumber = contact.ContactNumber,
                FileName = contact.FileName,
            };
            response.Data = contactDto;
            return response;
        }
        public int TotalContacts()
        {
            return _contactRepository.TotalContacts();
        }
        public ServiceResponse<string> AddContact(Contact contact, IFormFile file)
        {

            var response = new ServiceResponse<string>();
            if (contact == null)
            {
                response.Success = false;
                response.Message = "Something went wrong. Please try after sometime.";
                return response;
            }
            if (_contactRepository.ContactExists(contact.ContactId, contact.ContactNumber))
            {
                response.Success = false;
                response.Message = "Contact already exists.";
                return response;
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
            response.Success = result;
            response.Message = result ? "Contact saved successfully." : "Something went wrong. Please try after sometime.";

            return response;
        }

        public ServiceResponse<string> UpdateContact(Contact contact)
        {
            var response = new ServiceResponse<string>();
            if (contact == null)
            {
                response.Success = false;
                response.Message = "Something went wrong. Please try after sometime.";
                return response;
            }
            if (_contactRepository.ContactExists(contact.ContactId, contact.ContactNumber))
            {
                response.Success = false;
                response.Message = "Contact already exists.";
                return response;
            }

            var updatedContact =_contactRepository.GetContact(contact.ContactId);
            if(updatedContact != null)
            {
                updatedContact.FirstName = contact.FirstName;
                updatedContact.LastName = contact.LastName;
                updatedContact.Company = contact.Company;
                updatedContact.Email = contact.Email;
                updatedContact.ContactNumber = contact.ContactNumber;
                //updatedContact.FileName = contact.FileName;
                var result = _contactRepository.UpdateContact(updatedContact);
                response.Success = result;
                response.Message = result ? "Contact updated successfully." : "Something went wrong. Please try after sometime.";
            }
            else
            {
                response.Success = false;
                response.Message = "Something went wrong. Please try after sometime.";
                return response;
            }

            return response;
        }

        public ServiceResponse<string> DeleteContact(int contactId)
        {
            var response = new ServiceResponse<string>();

            if (contactId < 0)
            {
                response.Success = false;
                response.Message = "No record to delete.";

            }

            var result = _contactRepository.DeleteContact(contactId);
            response.Success = result;
            response.Message = result ? "COntact deleted successfully." : "Something went wrong, please try after sometime.";

            return response;
        }
    }
}
