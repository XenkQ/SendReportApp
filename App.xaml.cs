using System.Reflection;

namespace MauiApp1;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new MainPage();
    }

    public void LoadPage(Pages page)
    {
        var pageTypeFullName = GetType().Namespace + $".{page.GetPageName()}";
        var pageType = Assembly.GetExecutingAssembly().GetType(pageTypeFullName);

        if (pageType == null)
            throw new NullReferenceException($"Page type {pageType} is not exisitng");

        var newPage = (Page)Activator.CreateInstance(pageType)!;

        if(newPage != null)
            MainPage = newPage;
    }
}
