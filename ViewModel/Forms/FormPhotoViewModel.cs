using MauiApp1.Model;
using MauiApp1.Scripts;
using MauiApp1.Scripts.Data.Processors;
using MauiApp1.Scripts.GUI.ButtonHolders;
using MauiApp1.ViewModel.Forms;
using CommunityToolkit.Mvvm.ComponentModel;


#if ANDROID
using Android.Graphics;
using MauiApp1.Platforms.Android;
#endif

namespace MauiApp1.ViewModel.Forms;

public partial class FormPhotoViewModel : FormBaseViewModel
{
    private string _featuredPhotoPath = string.Empty;
    public Task _processingTask { get; private set; }
    public CancellationTokenSource CancellationTokenSource { get; private set; }

    private AlertDataToSend _alertDataToSend;

    [ObservableProperty]
    private ImageSource _featuredImageSource;

    public FormPhotoViewModel(AlertDataToSend alertDataToSend)
    {
        _alertDataToSend = alertDataToSend;
        //Do sprawdzenia
        FeaturedImageSource = ImageSource.FromFile("./Resources/no_photo.svg");
    }

    protected override void UpdateDataToSend()
    {
        _alertDataToSend.Base64Image = "New Image";
    }

    private async void OnTakePhotoClick(object sender, EventArgs e)
    {
        var photoFile = await TakePhoto();

        if (photoFile != null)
        {
            _featuredPhotoPath = photoFile.FullPath;
            FeaturedImageSource = ImageSource.FromFile(_featuredPhotoPath);
            _processingTask = StartProcessingDataInBackground();
        }
    }

    private async Task<FileResult?> TakePhoto()
    {
        try
        {
            return await MediaPicker.CapturePhotoAsync();
        }
        catch (Exception)
        {
            await Shell.Current.DisplayAlert("Error", $"Nie można zrobić zdjęcia! Upewnij się czy aplikacja ma uprawnienia do robienia zdjęć", "OK");
        }

        return null;
    }

    public Task GetProcessedTask() => _processingTask;

    public Task StartProcessingDataInBackground()
    {
        if (_processingTask != default)
        {
            CancellationTokenSource.Cancel();
            _processingTask.Wait();
        }

        CancellationTokenSource.TryReset();

#if ANDROID
        return Task.Run(() => ImageManipulator.GetImageAsBase64(_featuredPhotoPath,
                Bitmap.CompressFormat.Jpeg!, 100), CancellationTokenSource.Token)
            .ContinueWith(task => _alertDataToSend.Base64Image = task.Result);
#endif

        return Task.CompletedTask;
    }
}
