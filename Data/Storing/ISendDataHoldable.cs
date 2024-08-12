namespace MauiApp1.Data.Storing
{
    public interface ISendDataHoldable
    {
        public string Base64Image { get; set; }
        public int Category {  get; set; }
        public string Message { get; set; }
        public string IMEI { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}