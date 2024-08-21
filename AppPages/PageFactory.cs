using Microsoft.Extensions.Configuration;

namespace MauiApp1.AppPages
{
    internal static class PageFactory
    {
        public static Page CreatePage(PageConfiguration configuration)
             => (Page)Activator.CreateInstance(configuration.Page.ToPageType(configuration.App)!, configuration.App)!;
    }
}
