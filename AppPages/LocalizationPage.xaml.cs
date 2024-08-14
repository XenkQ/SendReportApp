using Mapsui.Projections;
using MauiApp1.GUI.FlowButtons;
using Mapsui.Extensions;
using Mapsui.Layers;
using Mapsui.Styles;
using Brush = Mapsui.Styles.Brush;
using Color = Mapsui.Styles.Color;
using Mapsui.UI.Maui;
using Mapsui;
using MauiApp1.AppPages;

namespace MauiApp1;

public partial class LocalizationPage : ContentPage, IFlowButtonHolder
{
    private const double STARTING_LATITUDE = 54.75851040001975;
    private const double STARTING_LONGITUDE = 17.55495071411133;

    private readonly IApp _app;
    private readonly MapControl _mapControl;
    private readonly MPoint _startLocation;

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
    }

    public void OnBackButtonClick(object sender, EventArgs e)
    {
        _app.LoadPage(Pages.DescriptionPage);
    }

    public async void OnNextButtonClick(object sender, EventArgs e)
    {
        if(_app.UserDataToSend.Longitude != default
            && _app.UserDataToSend.Latitude != default)
        {
            _app.LoadPage(Pages.SendingCompletedPage);
        }
        else
        {
            await DisplayAlert("Nie posiadamy twojej lokalizacji", "Lokalizacja jest niezbêdna dla zg³oszenia." +
                " Upewnij siê, ¿e wcisn¹³eœ przycisk \"Wyœlij lokalizacjê\"", "OK");
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
            var location = await Geolocation.GetLocationAsync(geolocationRequest);

            if (location != null)
            {
                _app.UserDataToSend.Longitude = location.Longitude;
                _app.UserDataToSend.Latitude = location.Latitude;

                var currentUserLocation = SphericalMercator.FromLonLat(
                    location.Longitude, location.Latitude).ToMPoint();

                _mapControl.Map.Navigator.PanLock = false;
                _mapControl.Map.Navigator.CenterOnAndZoomTo(currentUserLocation, 0.5f);

                var pinFeature = new PointFeature(currentUserLocation);
                var pinStyle = new SymbolStyle
                {
                    SymbolType = SymbolType.Ellipse,
                    Fill = new Brush(Color.Red),
                    Outline = new Pen(Color.Black, 2),
                    SymbolScale = 0.8
                };

                pinFeature.Styles.Add(pinStyle);
                var pinLayer = new MemoryLayer
                {
                    Name = "Pin Layer",
                    Features = [pinFeature]
                };
                _mapControl.Map.Layers.Add(pinLayer);

                LocalizationMap.Content = _mapControl;
                _mapControl.Map.Navigator.PanLock = true;
            }
        }
        else
        {
            await DisplayAlert("Nie mo¿na pobraæ lokalizacji urz¹dzenia!",
                "Lokalizacja jest niezbêdna w celu potwierdzenia zg³oszenia. Upewnij siê czy aplikacja ma uprawnienia dostêpu do lokalizacji", "OK");
        }
    }
}