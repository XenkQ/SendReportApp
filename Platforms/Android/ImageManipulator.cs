using Android.Graphics;

namespace MauiApp1.Platforms.Android;

internal static class ImageManipulator
{
    public const int MAX_IMAGE_SIZE = 1400;

    public static async Task<string> GetImageAsBase64(string path, Bitmap.CompressFormat compression, int quality = 100)
    {
        var bitmap = await GetBitmapFromPath(path);
        
        bitmap = GetResizedImageIfExtendingMaxSize(bitmap);

        var imageStream = PrepareImageStream(bitmap, compression, quality);

        return Convert.ToBase64String(imageStream.ToArray());
    }

    private static Bitmap GetResizedImageIfExtendingMaxSize(Bitmap bitmap)
    {
        if (bitmap.Width > MAX_IMAGE_SIZE
            || bitmap.Height > MAX_IMAGE_SIZE)
        {
            int newWidth = bitmap.Width;
            int newHeight = bitmap.Height;

            if (bitmap.Width > bitmap.Height)
            {
                newWidth = MAX_IMAGE_SIZE;
                newHeight = (int)(bitmap.Height * ((double)MAX_IMAGE_SIZE / bitmap.Width));
            }
            else if (bitmap.Width < bitmap.Height)
            {
                newHeight = MAX_IMAGE_SIZE;
                newWidth = (int)(bitmap.Width * ((double)MAX_IMAGE_SIZE / bitmap.Height));
            }

            return Bitmap.CreateScaledBitmap(bitmap, newWidth, newHeight, true);
        }

        return bitmap;
    }

    private static async Task<Bitmap> GetBitmapFromPath(string path, BitmapFactory.Options? options = null)
    {
        if (!File.Exists(path))
            throw new NullReferenceException($"File {path} is not existing");

        var bitmap = options != null ?
            await BitmapFactory.DecodeFileAsync(path) : await BitmapFactory.DecodeFileAsync(path, options);

        if (bitmap == null)
            throw new NullReferenceException($"Can't decode file {path} to bitmap");

        return bitmap;
    }

    private static MemoryStream PrepareImageStream(Bitmap bitmap, Bitmap.CompressFormat compression, int quality)
    {
        MemoryStream ms = new MemoryStream();
        bitmap.Compress(compression, quality, ms);
        return ms;
    }
}
