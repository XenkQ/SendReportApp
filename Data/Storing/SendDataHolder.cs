namespace MauiApp1.Data.Storing
{
    internal class SendDataHolder : ISendDataHoldable
    {
        public string IMEI { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string Localization { get; set; } = string.Empty;
        public string Base64Image { get; set; } = string.Empty;
        public int Category { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
