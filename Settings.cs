namespace MauiApp1;

public record ApiSettings(string BaseUrl, string StatusPath, string UploadPath);
public record SettingsRoot(ApiSettings ApiSettings);