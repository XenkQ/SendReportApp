using MauiApp1.GUI.FlowButtons;
using MauiApp1.AppPages;

#if ANDROID
using Android.Graphics;
using MauiApp1.Platforms.Android;
#endif

namespace MauiApp1;

public partial class PhotoPage : ContentPage, IFlowNextButtonHolder,
    IMustPrepareAfterLoad, IProcessDataInBackground
{
    private readonly IApp _app;
    private string _featuredPhotoPath = string.Empty;
    private string _base64Image = string.Empty;
    public Task<string> _processingTask { get; private set; }

    public PhotoPage(IApp app)
	{
		InitializeComponent();
        _app = app;
    }

    public async void OnNextButtonClick(object sender, EventArgs e)
    {
        if (_featuredPhotoPath != string.Empty)
        {
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
        var photoFile = await TakePhoto();

        if (photoFile != null)
        {
            _featuredPhotoPath = photoFile.FullPath;
            SetFeaturePhoto(_featuredPhotoPath);
            StartProcessingDataInBackground();
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

    public void StartProcessingDataInBackground()
    {
#if ANDROID
        _processingTask = Task.Run(() => ImageManipulator.GetImageResizedImageAsBase64(_featuredPhotoPath,
                Bitmap.CompressFormat.WebpLossless!, 100));
#endif
    }
}