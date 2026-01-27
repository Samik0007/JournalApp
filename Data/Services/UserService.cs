using Journal_App.Data.Abstractions;
using Journal_App.Data.Models;
using Journal_App.Data.Utils;
using Microsoft.EntityFrameworkCore;

namespace Journal_App.Data.Services;

public sealed class UserService(DBcontext context) : IUserService
{
    private readonly DBcontext _context = context;

    public Task<UserAuthMode> GetAuthModeAsync() =>
        _context.Users.AnyAsync().ContinueWith(t => t.Result ? UserAuthMode.LoginPassword : UserAuthMode.SetupPin);

    public async Task<User> CreateUserWithPinAsync(string pin)
    {
        if (pin.Length != 4 || !pin.All(char.IsDigit))
            throw new ArgumentException("PIN must be 4 digits.", nameof(pin));

        if (await _context.Users.AnyAsync())
            throw new InvalidOperationException("User already exists.");

        var user = new User
        {
            Id = Guid.NewGuid(),
            pin = int.Parse(pin),
            CreatedAt = DateTime.UtcNow
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<bool> VerifyPinAsync(string pin)
    {
        if (pin.Length != 4 || !pin.All(char.IsDigit))
            return false;

        var pinValue = int.Parse(pin);
        return await _context.Users.AnyAsync(u => u.pin == pinValue);
    }

    // Keep password methods minimal/simple for now
    public Task SetPasswordAsync(string password) => Task.CompletedTask;
    public Task<bool> VerifyPasswordAsync(string password) => Task.FromResult(true);
}
