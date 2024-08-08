using MauiApp1.Scripts;
using Microsoft.Maui.ApplicationModel;
using System.Text;

#if ANDROID
using Android.Graphics;
#endif

namespace MauiApp1;

public partial class PhotoPage : ContentPage, IFlowButtonHolder, IMustPrepareAfterLoad
{
    private static string? featuredPhotoPath = null;

	public PhotoPage()
	{
		InitializeComponent();
	}

    public void OnBackButtonClick(object sender, EventArgs e)
    {
        ((App)Application.Current!).LoadPage(Pages.MainPage);
    }

    public void OnNextButtonClick(object sender, EventArgs e)
    {
        ((App)Application.Current!).LoadPage(Pages.DescriptionPage);
    }

    public void PrepareAfterLoad()
    {
        if(featuredPhotoPath != null)
            SetFeaturePhoto(featuredPhotoPath);
    }

    private async void OnTakePhotoClick(object sender, EventArgs e)
    {
        try
        {
            var photoFile = await MediaPicker.CapturePhotoAsync();

            if (photoFile != null)
            {
                featuredPhotoPath = photoFile.FullPath;
                SetFeaturePhoto(featuredPhotoPath);
#if ANDROID
                string base64image = await Platforms.Android.ImageManipulator.GetImageAsBase64(photoFile, Bitmap.CompressFormat.Webp, 100);
                Console.WriteLine(base64image);
#endif
            }
        }
        catch(Exception)
        {
            await DisplayAlert("Error", $"Nie mo¿na wykonaæ zdjêcia! Upewnij siê czy aplikacja ma uprawnienia do robienia zdjêæ", "OK");
        }
    }

    private void SetFeaturePhoto(string path)
    {
        photoResultImage.Source = ImageSource.FromFile(path);
    }
}