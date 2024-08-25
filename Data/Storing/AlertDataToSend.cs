namespace MauiApp1.Data.Storing;

public sealed class AlertDataToSend
{
    public string UserId { get; set; } = "d4fb8586-101f-4dff-a91e-2488b8214ba3";
    public string Message { get; set; } = string.Empty;
    public string Base64Image { get; set; } = string.Empty;
    public int Category { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}
