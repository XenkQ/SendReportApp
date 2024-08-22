namespace MauiApp1.DTOs;

public record ApiSettings(string BaseUrl, string StatusPath, string UploadPath);
public record SettingsRoot(ApiSettings ApiSettings);