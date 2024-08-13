using Android.Media;

namespace MauiApp1.Platforms.Android
{
    internal static class ImageLocator
    {
        public static (double? latitude, double? longitude) GetImageLocation(string imagePath)
        {
            try
            {
                using var exifInterface = new ExifInterface(imagePath);

                Console.WriteLine($"Latitude: {exifInterface.GetAttribute(ExifInterface.TagGpsLatitude)}" +
                    $"\nLongitude: {exifInterface.GetAttribute(ExifInterface.TagGpsLongitude)}");

                //if (double.TryParse(exifInterface.GetAttribute(ExifInterface.TagGpsLatitude), out double latitude)
                //&& double.TryParse(exifInterface.GetAttribute(ExifInterface.TagGpsLongitude), out double longitude))
                //{
                //    Console.WriteLine($"Latitude: {latitude}\nLongitude: {longitude}");
                //    return (latitude, longitude);
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error extracting location: {ex.Message}");
            }

            return (null, null);
        }
    }
}
