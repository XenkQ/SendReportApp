using MauiApp1.Model;
using MauiApp1.Scripts;
using MauiApp1.Scripts.Data.Processors;
using MauiApp1.Scripts.GUI.ButtonHolders;
using MauiApp1.ViewModel.Forms;

#if ANDROID
using Android.Graphics;
using MauiApp1.Platforms.Android;
#endif

namespace MauiApp1.View.FormPages;

public partial class PhotoPage : ContentPage, IFlowNextButtonHolder
{
    private string _featuredPhotoPath = string.Empty;
    public Task _processingTask { get; private set; }
    public CancellationTokenSource CancellationTokenSource { get; private set; }

    public PhotoPage(FormPhotoViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
        CancellationTokenSource = new CancellationTokenSource();
    }

    public async void OnNextButtonClick(object sender, EventArgs e)
    {
        if (_featuredPhotoPath != string.Empty)
        {
            await Shell.Current.GoToAsync(nameof(DescriptionPage));
        }
        else
        {
            await DisplayAlert("Brak zdjęcia!", "Prosimy o zrobienie zdjęcia, gdyż jest ono podstawą zgłoszenia.", "OK");
        }
    }
}