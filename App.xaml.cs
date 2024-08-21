using MauiApp1.AppPages;
using MauiApp1.AppPages.Creation;
using MauiApp1.Connection;
using MauiApp1.Data.Sending;
using MauiApp1.Data.Storing;
using System.Collections.ObjectModel;

namespace MauiApp1;

public interface IApp
{
    ISendDataHoldable UserDataToSend { get; }
    ReadOnlyDictionary<Pages, Page> GetLoadedPages();
    void DisplayPage(Pages page);
    void ReloadPages();
}

public partial class App : Application, IApp
{
    public static string API_ENDPOINT = @"";
    public ISendDataHoldable UserDataToSend { get; private set; }
    private IServerConnectionChecker _serverConnectionChecker;
    private Dictionary<Pages, Page> _pages;
    private readonly IDataSender _dataSender;
    private IPagesPooler _pagesPooler;
    private Pages _startFormPage = Pages.PhotoPage;

    public App(IServerConnectionChecker serverConnectionChecker, IPagesPooler pagesPooler,
        ISendDataHoldable userDataToSend, IDataSender dataSender)
    {
        InitializeComponent();

        //Currently only light theme available
        UserAppTheme = AppTheme.Light;

        UserDataToSend = userDataToSend;
        _dataSender = dataSender;
        _serverConnectionChecker = serverConnectionChecker;
        _pagesPooler = pagesPooler;

        ReloadPages();
    }

    public void ReloadPages()
    {
        if (!NoConnectionDisplayer.DisplayIfNoConnection(this))
        {
            _pages = new(_pagesPooler.PoolAllPages(this));
            DisplayPage(_startFormPage);
        }
    }

    public ReadOnlyDictionary<Pages, Page> GetLoadedPages()
        => _pages.AsReadOnly();

    public void DisplayPage(Pages page)
    {
        if(!_pages.ContainsKey(page))
            throw new ArgumentException($"Can't load {page} becouse this page is not pooled");

        if (_pages[page] is IMustPrepareBeforeDisplay)
            ((IMustPrepareBeforeDisplay)_pages[page]).Prepare();

        MainPage = _pages[page];
    }
}
