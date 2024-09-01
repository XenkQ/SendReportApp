namespace MauiApp1.Model;

public record ApiSettings(string BaseUrl, string StatusPath, string UploadPath);
public record SettingsRoot(ApiSettings ApiSettings);