using Mapsui.Extensions;
using Mapsui.Projections;
using Mapsui.UI.Maui;

namespace MauiApp1.AppPages.Geolocation;

internal static class MapLocationDisplayer
{
    public static void DisplayLocationOnMap(ContentView viewDisplayingMap, MapControl mapControl,
        Location? locationToDisplay)
    {
        var locationOnMap = SphericalMercator.FromLonLat(
            locationToDisplay.Longitude, locationToDisplay.Latitude).ToMPoint();

        mapControl.Map.Navigator.PanLock = false;
        mapControl.Map.Navigator.CenterOnAndZoomTo(locationOnMap, 0.5f);

        Pin.AddToMap(mapControl.Map, locationOnMap, Pin.DefaultPinStyle);

        viewDisplayingMap.Content = mapControl;
        mapControl.Map.Navigator.PanLock = true;
    }
}
