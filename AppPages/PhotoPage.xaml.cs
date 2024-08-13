﻿using MauiApp1.GUI.FlowButtons;
using MauiApp1.AppPages;

#if ANDROID
using Android.Graphics;
using MauiApp1.Platforms.Android;
#endif

namespace MauiApp1;

public partial class PhotoPage : ContentPage, IFlowNextButtonHolder, IMustPrepareAfterLoad
{
    private readonly IApp _app;
    private string _featuredPhotoPath = string.Empty;
    private string _base64Image = string.Empty;

	public PhotoPage(IApp app)
	{
		InitializeComponent();
        _app = app;
	}

    public async void OnNextButtonClick(object sender, EventArgs e)
    {
        if (_base64Image != string.Empty)
        {
            _app.UserDataToSend.Base64Image = _base64Image;
#if ANDROID
        var (latitude, logitude) = ImageLocator.GetImageLocation(_featuredPhotoPath);
        if(latitude != null && logitude != null)
        {
            _app.UserDataToSend.Latitude = (double)latitude;
            _app.UserDataToSend.Longitude = (double)logitude;
        }
#endif
            _app.LoadPage(Pages.CategoryPage);
        }
        else
        {
            await DisplayAlert("Brak zdjęcia!", "Prosimy o zrobienie zdjęcia, gdyż jest ono podstawą zgłoszenia.", "OK");
        }
    }

    public void PrepareAfterLoad()
    {
        if(_featuredPhotoPath != string.Empty)
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
                _base64Image = await ImageManipulator.GetImageResizedImageAsBase64(
                    _featuredPhotoPath, Bitmap.CompressFormat.WebpLossless!, 100);
#endif
            }
        }
        catch(Exception)
        {
            await DisplayAlert("Error", $"Nie można zrobić zdjęcia! Upewnij się czy aplikacja ma uprawnienia do robienia zdjęć", "OK");
        }
    }

    private void SetFeaturePhoto(string path)
    {
        photoResultImage.Source = ImageSource.FromFile(path);
    }
}