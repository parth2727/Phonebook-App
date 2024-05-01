using PhonebookApplication.ViewModels;

namespace PhonebookApplication.Services.Contract
{
    public interface IAuthService
    {
        string RegisterUserService(RegisterViewModel register);
        string LoginUserService(LoginViewModel login);
    }
}
