namespace MauiApp1.DTOs;

public record ApiSettings(string BaseUrl, string StatusPath);
public record SettingsRoot(ApiSettings ApiSettings);