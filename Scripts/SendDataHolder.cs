namespace MauiApp1.Scripts
{
    internal class SendDataHolder : ISendDataHoldable
    {
        public string IMEI { get; set; }
        public string Wiadomosc { get; set; }
        public string Localization { get; set; }
        public string Base64Image { get; set; }

        public SendDataHolder()
        {
            IMEI = "";
            Wiadomosc = "";
            Localization = "";
            Base64Image = "";
        }

        public SendDataHolder(string imei, string wiadomosc, string localization, string base64Image)
        {
            IMEI = imei;
            Wiadomosc = wiadomosc;
            Localization = localization;
            Base64Image = base64Image;
        }
    }
}
