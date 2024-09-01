using Mapsui.Extensions;
using Mapsui.Projections;
using Mapsui.UI.Maui;

namespace MauiApp1.Scripts.Geolocation;

internal static class MapLocationDisplayer
{
    public static void DisplayLocationOnMap(MapControl mapControl, Location? locationToDisplay,
        float zoomOnLocation = 0.5f)
    {
        var locationOnMap = SphericalMercator.FromLonLat(
            locationToDisplay.Longitude, locationToDisplay.Latitude).ToMPoint();

        mapControl.Map.Navigator.PanLock = false;
        mapControl.Map.Navigator.CenterOnAndZoomTo(locationOnMap, zoomOnLocation);

        PinLayer.DisplaySinglePinOnMap(mapControl.Map, locationOnMap, PinLayer.DefaultPinStyle);

        //viewDisplayingMap.Content = null; //detaching content for event to be called
        //viewDisplayingMap.Content = mapControl;

        mapControl.Map.Navigator.PanLock = true;
    }
}
