﻿namespace MauiApp1.Scripts
{
    public interface ISendDataHoldable
    {
        public string IMEI { get; set; }
        public string Message { get; set; }
        public string Localization { get; set; }
        public string Base64Image { get; set; }
    }
}