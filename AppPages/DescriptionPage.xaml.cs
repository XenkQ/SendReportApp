using MauiApp1.AppPages;
using MauiApp1.GUI.FlowButtons;

namespace MauiApp1;

public partial class DescriptionPage : ContentPage, IFlowBackButtonsHolder
{
    private readonly IApp _app;

    public DescriptionPage(IApp app)
	{
		InitializeComponent();
        _app = app;
	}

    public void OnBackButtonClick(object sender, EventArgs e)
    {
        _app.LoadPage(Pages.CategoryPage);
    }

    private async void OnSendButtonClick(object sender, EventArgs e)
    {
        var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
        if (status != PermissionStatus.Granted)
        {
            status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
        }

        if (status == PermissionStatus.Granted)
        {
            GeolocationRequest geolocationRequest = new GeolocationRequest(GeolocationAccuracy.Best);
            var location = await Geolocation.GetLocationAsync(geolocationRequest);
            
            if (location != null)
            {
                _app.UserDataToSend.Latitude = location.Latitude;
                _app.UserDataToSend.Longitude = location.Longitude;

                if (DescriptionField.Text != string.Empty)
                    _app.UserDataToSend.Message = DescriptionField.Text;

                _app.LoadPage(Pages.SendingCompletedPage);
            }
        }
        else
        {
            await DisplayAlert("Nie można pobrać lokalizacji urządzenia!",
                "Lokalizacja jest niezbędna w celu potwierdzenia zgłoszenia. Upewnij się, że aplikacja ma dostęp do lokalizacji", "OK");
        }
    }
}