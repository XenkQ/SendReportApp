namespace MauiApp1.Scripts
{
    internal class SendDataHolder : ISendDataHoldable
    {
        public string IMEI { get; set; }
        public string Message { get; set; }
        public string Localization { get; set; }
        public string Base64Image { get; set; }

        public SendDataHolder()
        {
            IMEI = "";
            Message = "";
            Localization = "";
            Base64Image = "";
        }

        public SendDataHolder(string imei, string wiadomosc, string localization, string base64Image)
        {
            IMEI = imei;
            Message = wiadomosc;
            Localization = localization;
            Base64Image = base64Image;
        }
    }
}
