using PhonebookApplication.Models;

namespace PhonebookApplication.Data.Contract
{
    public interface IAuthRepository
    {
        bool RegisterUser(User user);
        User ValidateUser(string username);
        bool UserExists(string loginId, string email);
    }
}
