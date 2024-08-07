namespace MauiApp1;

internal static class PagesExtensions
{
    public static string GetPageName(this Pages page) => page switch
    {
        Pages.MainPage => "MainPage",
        Pages.PhotoPage => "PhotoPage",
        Pages.DescriptionPage => "DescriptionPage",
        _ => throw new ArgumentNullException($"page of enum: {page} is not implemented"),
    };
}
