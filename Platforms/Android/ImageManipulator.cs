using Android.Graphics;

namespace MauiApp1.Platforms.Android
{
    internal static class ImageManipulator
    {
        public static async Task<string> GetImageAsBase64(FileResult image, Bitmap.CompressFormat compression, int quality)
        {
            using MemoryStream ms = new MemoryStream();
            var bitmap = BitmapFactory.DecodeFile(image.FullPath);
            bitmap.Compress(Bitmap.CompressFormat.Webp, 100, ms);
            return Convert.ToBase64String(ms.ToArray());
        }
    }
}
