using Android.Graphics;

namespace MauiApp1.Platforms.Android
{
    internal static class ImageManipulator
    {
        public const int MAX_IMAGE_WIDTH = 1400;
        public const int MAX_IMAGE_HEIGHT = 1400;

        public static async Task<string> GetImageAsBase64(string path, Bitmap.CompressFormat compression, int quality = 100)
        {
            var bitmap = await BitmapFactory.DecodeFileAsync(path);

            return GetCompressedBitmapInBase64(bitmap, compression, quality);
        }

        public static async Task<string> GetImageResizedImageAsBase64(string path, Bitmap.CompressFormat compression, int quality = 100,
            int maxWidth = MAX_IMAGE_HEIGHT, int maxHeight = MAX_IMAGE_HEIGHT)
        {
            var bitmap = await GetResizedBitmapFromPath(path);

            return GetCompressedBitmapInBase64(bitmap, compression, quality);
        }

        private static string GetCompressedBitmapInBase64(Bitmap bitmap, Bitmap.CompressFormat compression, int quality)
        {
            using MemoryStream ms = new MemoryStream();
            bitmap.Compress(compression, quality, ms);

            return Convert.ToBase64String(ms.ToArray());
        }

        private static async Task<Bitmap?> GetResizedBitmapFromPath(string path, int maxWidth = MAX_IMAGE_HEIGHT,
            int maxHeight = MAX_IMAGE_HEIGHT)
        {
            var options = new BitmapFactory.Options() { InJustDecodeBounds = true};
            var bitmap = await GetBitmapFromPath(path, options);
            options.InSampleSize = CalculateInSampleSize(options, maxWidth, maxHeight);
            options.InJustDecodeBounds = false;

            return await GetBitmapFromPath(path, options);
        }

        private static async Task<Bitmap?> GetBitmapFromPath(string path, BitmapFactory.Options? options = null)
        {
            if (!File.Exists(path)) throw new NullReferenceException($"File {path} is not existing");

            var bitmap = options != null ?
                await BitmapFactory.DecodeFileAsync(path) : await BitmapFactory.DecodeFileAsync(path, options);

            if (bitmap == null) throw new NullReferenceException($"Can't decode file {path} to bitmap");

            return bitmap;
        }

        private static int CalculateInSampleSize(BitmapFactory.Options options, int requestedWidth, int requestedHeight)
        {
            int height = options.OutHeight;
            int width = options.OutWidth;
            int inSampleSize = 1;

            if (height > requestedHeight || width > requestedWidth)
            {
                int halfHeight = height / 2;
                int halfWidth = width / 2;

                while ((halfHeight / inSampleSize) >= requestedHeight
                       && (halfWidth / inSampleSize) >= requestedWidth)
                {
                    inSampleSize *= 2;
                }
            }

            return inSampleSize;
        }
    }
}
