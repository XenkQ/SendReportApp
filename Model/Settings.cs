using System.Text.Json.Serialization;

namespace MauiApp1.Model.Settings;

public record ApiSettings(
    [property: JsonPropertyName("base_url")] string BaseUrl,
    [property: JsonPropertyName("status_path")] string StatusPath,
    [property: JsonPropertyName("upload_path")] string UploadPath
);

public record SettingsRoot(
    [property: JsonPropertyName("api_setting")] ApiSettings ApiSettings
);