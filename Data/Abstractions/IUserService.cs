using Journal_App.Data.Models;

namespace Journal_App.Data.Abstractions;

public enum UserAuthMode
{
    SetupPin,
    LoginPassword
}

public interface IUserService
{
    Task<UserAuthMode> GetAuthModeAsync();

    Task<User> CreateUserWithPinAsync(string pin);

    Task SetPasswordAsync(string password);

    Task<bool> VerifyPinAsync(string pin);

    Task<bool> VerifyPasswordAsync(string password);
}
