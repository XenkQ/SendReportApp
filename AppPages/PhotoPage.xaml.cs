using MauiApp1.GUI.FlowButtons;
using MauiApp1.AppPages;
using MauiApp1.Data.Waiting;

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
    private IWaitForData<string> _dataWaiter;

    public PhotoPage(IApp app, IWaitForData<string> dataWaiter)
	{
		InitializeComponent();
        _app = app;
        _dataWaiter = dataWaiter;
    }

    public void NotifyAfterDataProcesing<T>(T processedData, IWaitForData<T> dataWaiter)
        => dataWaiter.OnNotification(processedData);

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
        try
        {
            var photoFile = await MediaPicker.CapturePhotoAsync();

            if (photoFile != null)
            {
                _featuredPhotoPath = photoFile.FullPath;
                SetFeaturePhoto(_featuredPhotoPath);
#if ANDROID
                _app.AddTask(Task.Run(() => ImageManipulator.GetImageResizedImageAsBase64(_featuredPhotoPath,
                    Bitmap.CompressFormat.WebpLossless!, 100)));
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