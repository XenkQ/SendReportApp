using MauiApp1.Scripts;
using System.Reflection;

namespace MauiApp1;

public partial class App : Application, IApp
{
    private Dictionary<Pages, Page> _pages;

    public ISendDataHoldable UserDataToSend { get; private set; }

    public App(ISendDataHoldable userDataToSend)
    {
        InitializeComponent();

        _pages = new Dictionary<Pages, Page>();

        CreatePagesOnStart();

        UserDataToSend = userDataToSend;

        MainPage = CreatePage(Pages.PhotoPage);
    }

    private void CreatePagesOnStart()
    {
        foreach (Pages page in Enum.GetValues(typeof(Pages)))
            _pages.Add(page, CreatePage(page)!);
    }

    private Page? CreatePage(Pages page)
    {
        var pageTypeFullName = GetType().Namespace + $".{page}";
        var pageType = Assembly.GetExecutingAssembly().GetType(pageTypeFullName);

        if (pageType == null)
            throw new NullReferenceException($"Page type {pageType} is not exisitng");

        return (Page)Activator.CreateInstance(pageType, this)!;
    }

    public void LoadPage(Pages page)
    {
        MainPage = _pages[page];

        if (MainPage is IMustPrepareAfterLoad)
            ((IMustPrepareAfterLoad)MainPage).PrepareAfterLoad();
    }
}
