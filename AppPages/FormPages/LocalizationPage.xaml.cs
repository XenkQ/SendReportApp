using Mapsui.Projections;
using MauiApp1.GUI.FlowButtons;
using Mapsui.Extensions;
using Mapsui.UI.Maui;
using Mapsui;
using MauiApp1.AppPages;
using MauiApp1.AppPages.Geolocation;
using CommunityToolkit.Maui.Views;

namespace MauiApp1;

public partial class LocalizationPage : ContentPage, IFlowBackButtonHolder, ISubmitButtonHolder
{
    private const double STARTING_LATITUDE = 54.75851040001975;
    private const double STARTING_LONGITUDE = 17.55495071411133;

    private readonly IApp _app;
    private readonly MapControl _mapControl;
    private readonly MPoint _startLocation;

    private LoadingPopup _loadingPopup = new();
    private bool _IsLoading;

    public LocalizationPage(IApp app)
	{
		InitializeComponent();
        _app = app;

        _mapControl = new MapControl();
        _mapControl.Map?.Layers.Add(Mapsui.Tiling.OpenStreetMap.CreateTileLayer());
        _startLocation = SphericalMercator.FromLonLat(STARTING_LONGITUDE, STARTING_LATITUDE).ToMPoint();
        _mapControl.Map.Navigator.CenterOnAndZoomTo(_startLocation, 2f);
        _mapControl.Map.Navigator.PanLock = true;
        
        LocalizationMap.Content = _mapControl;
        MapContentChangeBehavior.ContentChanged += OnContentChange!;
    }

    public void OnBackButtonClick(object sender, EventArgs e)
    {
        _app.DisplayPage(Pages.DescriptionPage);
    }

    public async void OnSubmitButtonClick(object sender, EventArgs e)
    {
        if(_app.UserDataToSend.Longitude != default
            && _app.UserDataToSend.Latitude != default)
        {
            _app.DisplayPage(Pages.FormSendLoadingPage);
        }
        else
        {
            await DisplayAlert("Nie posiadamy twojej lokalizacji", "Lokalizacja jest niezbędna dla przyjęcia zgłoszenia." +
                " Upewnij się, że lokalizacja została udostępniona poprzez kliknięcie przycisku \"Wyślij Lokalizację\"" +
                " i wyrażeniu zgody na dokładną lokalizację.", "OK");
        }
    }

    public async void OnGetLocalizationBtnClick(object sender, EventArgs e)
    {
        var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
        if (status != PermissionStatus.Granted)
        {
            status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
        }

        if (status == PermissionStatus.Granted)
        {
            GeolocationRequest geolocationRequest = new GeolocationRequest(GeolocationAccuracy.Best);

            this.ShowPopup(_loadingPopup);

            var location = await Geolocation.GetLocationAsync(geolocationRequest);

            if (location != null)
            {
                _app.UserDataToSend.Longitude = location.Longitude;
                _app.UserDataToSend.Latitude = location.Latitude;

                _IsLoading = true;

                MapLocationDisplayer.DisplayLocationOnMap(LocalizationMap, _mapControl, location);
            }
        }
        else
        {
            await DisplayAlert("Nie można pobrać lokalizacji urządzenia!",
                "Lokalizacja jest niezbędna w celu potwierdzenia zgłoszenia. Upewnij się że aplikacja ma uprawnienia dostępu do lokalizacji", "OK");
        }
    }

    public void OnContentChange(object sender, EventArgs e)
    {
        if(_IsLoading)
        {
            _loadingPopup.Close();
            _IsLoading = false;
            _loadingPopup = new LoadingPopup();
        }
    }
}