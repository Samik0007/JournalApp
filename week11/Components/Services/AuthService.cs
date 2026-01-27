using System;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;

namespace week11.Components.Services;

internal sealed class AuthService
{
    private const string PinKey = "auth_pin";
    private const string UsernameKey = "auth_username";
    private const string LockStateKey = "auth_lock";

    public async Task SavePinAsync(string pin)
    {
        if (string.IsNullOrWhiteSpace(pin))
        {
            throw new ArgumentException("PIN must not be empty.", nameof(pin));
        }

        await SecureStorage.SetAsync(PinKey, pin);
    }

    public async Task<bool> VerifyPinAsync(string pin)
    {
        if (string.IsNullOrEmpty(pin))
        {
            return false;
        }

        var storedPin = await SecureStorage.GetAsync(PinKey);
        return !string.IsNullOrEmpty(storedPin) && storedPin == pin;
    }

    public async Task<bool> HasPinAsync()
    {
        var storedPin = await SecureStorage.GetAsync(PinKey);
        return !string.IsNullOrEmpty(storedPin);
    }

    public void SaveUsername(string username)
    {
        if (string.IsNullOrWhiteSpace(username))
        {
            throw new ArgumentException("Username must not be empty.", nameof(username));
        }

        Preferences.Set(UsernameKey, username);
    }

    public string GetUsername()
    {
        return Preferences.Get(UsernameKey, string.Empty);
    }

    public void SetLocked(bool isLocked)
    {
        Preferences.Set(LockStateKey, isLocked ? "1" : "0");
    }

    public bool IsLocked()
    {
        return Preferences.Get(LockStateKey, "1") == "1";
    }
}
