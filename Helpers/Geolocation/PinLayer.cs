using Mapsui.Layers;
using Mapsui.Styles;
using Mapsui;
using Brush = Mapsui.Styles.Brush;
using Color = Mapsui.Styles.Color;

namespace MauiApp1.Scripts.Geolocation;

internal static class PinLayer
{
    private const string PIN_LAYER_NAME = "PinLayer Layer";
    public readonly static SymbolStyle DefaultPinStyle = new SymbolStyle
    {
        SymbolType = SymbolType.Ellipse,
        Fill = new Brush(Color.Red),
        Outline = new Pen(Color.Black, 2),
        SymbolScale = 0.8
    };

    public static void DisplaySinglePinOnMap(Mapsui.Map map, MPoint locationOnMap, SymbolStyle pinStyle)
    {
        var pinFeature = new PointFeature(locationOnMap);

        pinFeature.Styles.Add(pinStyle);
        var pinLayer = new MemoryLayer
        {
            Name = PIN_LAYER_NAME,
            Features = [pinFeature]
        };

        map.Layers.Remove(c => c.Name == PIN_LAYER_NAME);
        map.Layers.Add(pinLayer);
    }
}
