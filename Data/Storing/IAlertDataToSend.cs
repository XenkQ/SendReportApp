namespace MauiApp1.Data.Storing
{
    public interface IAlertDataToSend
    {
        public string Id { get; set; }
        public string Base64Image { get; set; }
        public int Category {  get; set; }
        public string Message { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}