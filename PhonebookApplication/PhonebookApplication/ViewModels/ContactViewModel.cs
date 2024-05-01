using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace PhonebookApplication.ViewModels
{
    public class ContactViewModel
    {
        [DisplayName("Contact id")]
        public int ContactId { get; set; }

        [DisplayName("First name")]
        [Required(ErrorMessage = "Firstname is required.")]
        [StringLength(50)]
        public string FirstName { get; set; }

        [DisplayName("Last name")]
        [Required(ErrorMessage = "Lastname is required.")]
        [StringLength(50)]
        public string LastName { get; set; }

        [DisplayName("Company")]
        [Required(ErrorMessage = "Company is required.")]
        [StringLength(50)]
        public string Comapany { get; set; }

        [Required(ErrorMessage = "Email address is required")]
        [StringLength(50)]
        [EmailAddress]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Invalid email format.")]
        [DisplayName("Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Contact Number is required")]
        [StringLength(15)]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?\d{3}\)?[-.\s]?\d{3}[-.\s]?\d{4}$", ErrorMessage = "Invalid contact number.")]
        [DisplayName("Contact Number")]
        public string ContactNumber { get; set; }

        public string FileName { get; set; } = string.Empty;
        [Required(ErrorMessage = "File is required.")]
        [DisplayName("File")]
        public IFormFile File { get; set; }
    }
}
