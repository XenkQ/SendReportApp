using MauiApp1.GUI.FlowButtons;
using MauiApp1.AppPages;
using MauiApp1.Data.Processors;

#if ANDROID
using Android.Graphics;
using MauiApp1.Platforms.Android;
#endif

namespace MauiApp1;

public partial class PhotoPage : ContentPage, IFlowNextButtonHolder,
    IMustPrepareBeforeDisplay, IProcessDataInBackground
{
    private readonly IApp _app;
    private string _featuredPhotoPath = string.Empty;
    public Task _processingTask { get; private set; }
    public CancellationTokenSource CancellationTokenSource { get; private set; }

    public PhotoPage(IApp app)
	{
		InitializeComponent();
        _app = app;
        CancellationTokenSource = new CancellationTokenSource();
    }

    public async void OnNextButtonClick(object sender, EventArgs e)
    {
        if (_featuredPhotoPath != string.Empty)
        {
            _app.DisplayPage(Pages.CategoryPage);
        }
        else
        {
            await DisplayAlert("Brak zdjęcia!", "Prosimy o zrobienie zdjęcia, gdyż jest ono podstawą zgłoszenia.", "OK");
        }
    }

    public void Prepare()
    {
        if(_featuredPhotoPath != string.Empty)
            SetFeaturePhoto(_featuredPhotoPath);
    }

    private async void OnTakePhotoClick(object sender, EventArgs e)
    {
        var photoFile = await TakePhoto();

        if (photoFile != null)
        {
            _featuredPhotoPath = photoFile.FullPath;
            SetFeaturePhoto(_featuredPhotoPath);
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
            await DisplayAlert("Error", $"Nie można zrobić zdjęcia! Upewnij się czy aplikacja ma uprawnienia do robienia zdjęć", "OK");
        }

        return null;
    }

    private void SetFeaturePhoto(string path)
       => photoResultImage.Source = ImageSource.FromFile(path);

    public Task GetProcessedTask() => _processingTask;

    public Task StartProcessingDataInBackground()
    {
        if(_processingTask != default)
        {
            CancellationTokenSource.Cancel();
            _processingTask.Wait();
        }

        CancellationTokenSource.TryReset();

#if ANDROID
        return Task.Run(() => ImageManipulator.GetImageAsBase64(_featuredPhotoPath,
                Bitmap.CompressFormat.Jpeg!, 100), CancellationTokenSource.Token)
            .ContinueWith(task => _app.UserDataToSend.Base64Image = task.Result);
#endif

        return Task.CompletedTask;
    }
}