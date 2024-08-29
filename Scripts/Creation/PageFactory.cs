using MauiApp1.Scripts;
using Microsoft.Extensions.Configuration;

namespace MauiApp1.Scripts.Creation;

internal static class PageFactory
{
    public static Page CreatePage(PageConfiguration configuration)
         => (Page)Activator.CreateInstance(configuration.Page.ToPageType(configuration.App)!, configuration.App)!;
}
