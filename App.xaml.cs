using MauiApp1.Scripts;
using System.Reflection;

namespace MauiApp1;

public partial class App : Application
{
    private Dictionary<Pages, Page> _pages;
    public ISendDataHoldable UserDataToSend;

    public App()
    {
        InitializeComponent();

        _pages = new Dictionary<Pages, Page>();

        CreatePagesOnStart();

        UserDataToSend = new SendDataHolder();

        MainPage = new MainPage();
    }

    private void CreatePagesOnStart()
    {
        foreach (Pages page in Enum.GetValues(typeof(Pages)))
            _pages.Add(page, CreatePage(page)!);
    }

    private Page? CreatePage(Pages page)
    {
        var pageTypeFullName = GetType().Namespace + $".{page.GetPageName()}";
        var pageType = Assembly.GetExecutingAssembly().GetType(pageTypeFullName);

        if (pageType == null)
            throw new NullReferenceException($"Page type {pageType} is not exisitng");

        return (Page)Activator.CreateInstance(pageType)!;
    }

    public void LoadPage(Pages page)
    {
        MainPage = _pages[page];

        if(MainPage is IMustPrepareAfterLoad)
            ((IMustPrepareAfterLoad)MainPage).PrepareAfterLoad();
    }
}
