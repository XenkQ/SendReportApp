using Mapsui.UI.Maui;
using Mapsui;
using MauiApp1.Model;
using MauiApp1.Scripts.Geolocation;
using MauiApp1.Services;
using MauiApp1.View.StatusPages;
using Mapsui.Projections;
using Mapsui.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp1.Scripts.Processors;
using MauiApp1.Resources.Languages;

namespace MauiApp1.ViewModel.Forms;

public partial class FormLocalizationViewModel : FormBaseViewModel,
    IUpdateAlertData<(double longitude, double latitude)>
{
    private const double START_LATITUDE = 54.75851040001975;
    private const double START_LONGITUDE = 17.55495071411133;
    private const float START_ZOOM = 2f;

    private readonly AlertDataToSend _alertDataToSend;
    private readonly IDialogService _dialogService;
    private readonly ILoadingPopupService _loadingPopupService;
    private readonly INoConnectionDisplayer _noConnectionDisplayer;
    private readonly MPoint _startLocation;

    [ObservableProperty]
    private MapControl _localizationMapControl;

    public FormLocalizationViewModel(AlertDataToSend alertDataToSend, IDialogService dialogService,
        ILoadingPopupService loadingPopupService, INoConnectionDisplayer noConnectionDisplayer)
    {
        Title = AppResources.Localization;
        _alertDataToSend = alertDataToSend;
        _dialogService = dialogService;
        _loadingPopupService = loadingPopupService;
        _noConnectionDisplayer = noConnectionDisplayer;
        _startLocation = SphericalMercator.FromLonLat(START_LONGITUDE, START_LATITUDE).ToMPoint();
        LocalizationMapControl = CreateStartMapControl();
    }

    private MapControl CreateStartMapControl()
    {
        var mapControl = new MapControl();
        mapControl.Map?.Layers.Add(Mapsui.Tiling.OpenStreetMap.CreateTileLayer());
        mapControl.Map.Navigator.CenterOnAndZoomTo(_startLocation, START_ZOOM);
        mapControl.Map.Navigator.PanLock = true;
        return mapControl;
    }

    protected override async Task ToNextFormAsync()
    {
        if (_noConnectionDisplayer.DisplayIfNoConnection()) return;

        if (_alertDataToSend.Longitude != default
            && _alertDataToSend.Latitude != default)
        {
            await Shell.Current.GoToAsync(nameof(ReportSendLoadingPage));
        }
        else
        {
            await _dialogService.ShowAlertAsync(AppResources.NoLocalization, AppResources.LocalizationMessage, AppResources.OK);
        }
    }

    [RelayCommand]
    private async Task SendLocalization()
    {
        var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
        if (status != PermissionStatus.Granted)
            status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();

        if (status == PermissionStatus.Granted)
        {
            try
            {
                GeolocationRequest geolocationRequest = new GeolocationRequest(GeolocationAccuracy.Best);

                _loadingPopupService.ShowLoadingPopup(AppResources.Wait);

                var location = await Geolocation.GetLocationAsync(geolocationRequest);

                if (location != null)
                {
                    UpdateAlertData((location.Longitude, location.Latitude));

                    MapLocationDisplayer.DisplayLocationOnMap(LocalizationMapControl, location);
                }

            }
            catch (PermissionException ex)
            {
                await _dialogService.ShowAlertAsync(AppResources.CantGetLocalization,
                    AppResources.LocalizationIsRequired, AppResources.OK);
            }
            catch (FeatureNotEnabledException ex)
            {
                await _dialogService.ShowAlertAsync(AppResources.LocalizadionIsDisabled,
                    AppResources.EnableLocalization, AppResources.OK);
            }
            catch (Exception ex)
            {
                await _dialogService.ShowAlertAsync(AppResources.UnsuportedErrorOccured,
                    AppResources.TryRestartApp, AppResources.OK);
            }
        }
        else
        {
            await _dialogService.ShowAlertAsync(AppResources.CantGetLocalization,
                AppResources.LocalizationIsRequired, AppResources.OK);
        }

        _loadingPopupService.CloseLoadingPopup();
    }

    public void UpdateAlertData((double longitude, double latitude) input)
    {
        _alertDataToSend.Longitude = input.longitude;
        _alertDataToSend.Latitude = input.latitude;
    }
}
