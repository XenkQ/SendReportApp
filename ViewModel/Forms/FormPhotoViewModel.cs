using MauiApp1.Model;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp1.View.FormPages;
using MauiApp1.Services;

using MauiApp1.Scripts.Processors;

#if ANDROID
using Android.Graphics;
using MauiApp1.Platforms.Android;
#endif

namespace MauiApp1.ViewModel.Forms;

public partial class FormPhotoViewModel : FormBaseViewModel, IProcessDataInBackground,
    IUpdateAlertData<string>
{
    public Task ProcessedTask { get; private set; }
    public CancellationTokenSource CancellationTokenSource { get; private set; }
    private string _featuredPhotoPath = string.Empty;
    private readonly AlertDataToSend _alertDataToSend;
    private readonly IDialogService _dialogService;

    [ObservableProperty]
    private ImageSource _featuredImageSource;

    public FormPhotoViewModel(AlertDataToSend alertDataToSend, IDialogService dialogService,
        IFormBackgroundTaskObserver formBackgroundTaskObserver)
    {
        Title = "Zdjęcie";
        _alertDataToSend = alertDataToSend;
        _dialogService = dialogService;
        formBackgroundTaskObserver.Subscribe(this);
        CancellationTokenSource = new CancellationTokenSource();
        FeaturedImageSource = ImageSource.FromFile("no_photo.png");
    }

    [RelayCommand]
    private async void TakePhoto()
    {
        var photoFile = await CapturePhotoFromDevice();

        if (photoFile != null)
        {
            _featuredPhotoPath = photoFile.FullPath;
            FeaturedImageSource = ImageSource.FromFile(_featuredPhotoPath);
        }
    }

    private async Task<FileResult?> CapturePhotoFromDevice()
    {
        try
        {
            return await MediaPicker.CapturePhotoAsync();
        }
        catch (Exception)
        {
            await _dialogService.ShowAlertAsync("Error", $"Nie można zrobić zdjęcia! Upewnij się czy aplikacja ma uprawnienia do robienia zdjęć", "OK");
        }

        return null;
    }

    public Task GetProcessedTask() => ProcessedTask;

    public Task StartProcessingDataInBackground()
    {
        if (ProcessedTask != default)
        {
            CancellationTokenSource.Cancel();
            ProcessedTask.Wait();
        }

        CancellationTokenSource = new CancellationTokenSource();

#if ANDROID
        return Task.Run(() => ImageManipulator.GetImageAsBase64(_featuredPhotoPath,
                Bitmap.CompressFormat.Jpeg!, 100), CancellationTokenSource.Token)
            .ContinueWith(task => UpdateAlertData(task.Result), TaskContinuationOptions.OnlyOnRanToCompletion);
#endif

        return Task.CompletedTask;
    }

    protected override async Task ToNextFormAsync()
    {
        if (_featuredPhotoPath != string.Empty)
        {
            ProcessedTask = StartProcessingDataInBackground();
            await Shell.Current.GoToAsync(nameof(CategoryPage));
        }
        else
        {
            await _dialogService.ShowAlertAsync("Brak zdjęcia!", "Prosimy o zrobienie zdjęcia, gdyż jest ono podstawą zgłoszenia.", "OK");
        }
    }

    protected override async Task ToPreviousFormAsync()
        => await Task.CompletedTask;

    public void UpdateAlertData(string input)
        => _alertDataToSend.Base64Image = input;
}
