using Android.App;
using Android.Runtime;
using Android.Graphics;
using Android.Provider;
using Java.IO;

namespace MauiApp1;

[Application]
public class MainApplication : MauiApplication
{
    public MainApplication(IntPtr handle, JniHandleOwnership ownership)
        : base(handle, ownership)
    {
    }

    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

    public static async Task TakePhoto()
    {
        try
        {
            var photo = await MediaPicker.CapturePhotoAsync();

            if (photo != null)
            {
                //using (var stream = await photo.OpenReadAsync())
                //{
                //    photoResultImage.Source = ImageSource.FromStream(() => stream);
                //}

                using (var stream = await photo.OpenReadAsync())
                {
                    using MemoryStream ms = new MemoryStream();
                    var bitmap = BitmapFactory.DecodeFile(photo.FullPath);
                    var byteArrayOutputStream = new ByteArrayOutputStream();
                    bitmap.Compress(Bitmap.CompressFormat.Png, 100, ms);
                    var byteArray = ms.ToArray();
                    System.Console.WriteLine(Convert.ToBase64String(byteArray));
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}
