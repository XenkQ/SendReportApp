using MauiApp1.Scripts;

#if ANDROID
using Android.Graphics;
#endif

namespace MauiApp1;

public partial class PhotoPage : ContentPage, IFlowButtonHolder, IMustPrepareAfterLoad
{
    private readonly IApp _app;
    private static string? _featuredPhotoPath = null;
    private string _base64Image;

	public PhotoPage(IApp app)
	{
		InitializeComponent();
        _app = app;
	}

    public void OnBackButtonClick(object sender, EventArgs e)
    {
        _app.LoadPage(Pages.MainPage);
    }

    public void OnNextButtonClick(object sender, EventArgs e)
    {
        _app.UserDataToSend.Base64Image = _base64Image;
        _app.LoadPage(Pages.DescriptionPage);
    }

    public void PrepareAfterLoad()
    {
        if(_featuredPhotoPath != null)
            SetFeaturePhoto(_featuredPhotoPath);
    }

    private async void OnTakePhotoClick(object sender, EventArgs e)
    {
        try
        {
            var photoFile = await MediaPicker.CapturePhotoAsync();

            if (photoFile != null)
            {
                _featuredPhotoPath = photoFile.FullPath;
                SetFeaturePhoto(_featuredPhotoPath);
#if ANDROID
                _base64Image = await Platforms.Android.ImageManipulator.GetImageResizedImageAsBase64(
                    _featuredPhotoPath, Bitmap.CompressFormat.Webp, 100);
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