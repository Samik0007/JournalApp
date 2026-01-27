namespace Journal_App.Data.Abstractions;

public enum AppThemeMode
{
    Light,
    Dark
}

public interface IThemeService
{
    AppThemeMode GetTheme();
    void SetTheme(AppThemeMode theme);
}
