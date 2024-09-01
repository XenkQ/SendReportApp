using System.Reflection;

namespace MauiApp1.Scripts;

internal static class PagesExtensions
{
    public static Type ToPageType(this Pages simplePage, IApp app)
    {
        var pageTypeFullName = app.GetType().Namespace + $".{simplePage}";
        var pageType = Assembly.GetExecutingAssembly().GetType(pageTypeFullName);

        if (pageType == null)
            throw new NullReferenceException($"Page type {pageType} is not exisitng");

        return pageType;
    }
}