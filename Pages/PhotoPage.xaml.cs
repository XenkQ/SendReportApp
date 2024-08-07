using MauiApp1.Scripts;
using Microsoft.Maui.ApplicationModel;
using System.Text;

namespace MauiApp1;

public partial class PhotoPage : ContentPage, IFlowButtonHolder
{
	public PhotoPage()
	{
		InitializeComponent();
	}

    public void OnBackButtonClick(object sender, EventArgs e)
    {
        ((App)Application.Current).LoadPage(Pages.MainPage);
    }

    public void OnNextButtonClick(object sender, EventArgs e)
    {
        ((App)Application.Current).LoadPage(Pages.DescriptionPage);
    }

    private async void OnTakePhotoClick(object sender, EventArgs e)
    {
        try
        {
#if ANDROID
            await MainApplication.TakePhoto();
#endif
        }
        catch(Exception) 
        {
            await DisplayAlert("Error", $"Nie mo¿na wykonaæ zdjêcia! Upewnij siê czy aplikacja ma uprawnienia do robienia zdjêæ", "OK");
        }
    }
}