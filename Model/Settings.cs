namespace MauiApp1.Model.Settings;

public record ApiSettings(string BaseUrl, string StatusPath, string UploadPath);
public record SettingsRoot(ApiSettings ApiSettings);