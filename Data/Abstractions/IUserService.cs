using Journal_App.Data.Models;

namespace Journal_App.Data.Abstractions;

public enum UserAuthMode
{
    SetupPin,
    LoginPin
}

public interface IUserService
{
    Task<UserAuthMode> GetAuthModeAsync();

    Task<User> CreateUserWithPinAsync(string pin);

    Task<bool> VerifyPinAsync(string pin);

    Task ChangePinAsync(string currentPin, string newPin);
}
