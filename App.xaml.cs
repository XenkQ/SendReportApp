using MauiApp1.Model;
using MauiApp1.Scripts;
using MauiApp1.Scripts.Connection;
using MauiApp1.Scripts.Creation;
using MauiApp1.Services;
using MonkeyFinder;
using System.Collections.ObjectModel;

namespace MauiApp1;

public interface IApp
{
    IAlertDataSender DataSender { get; init; }
    SettingsRoot SettingsRoot { get; }
    AlertDataToSend UserDataToSend { get; init; }
    ReadOnlyDictionary<Pages, Page> GetLoadedPages();
    void DisplayPage(Pages page);
    void RefreshPages();
}

public partial class App : Application, IApp
{
    public IAlertDataSender DataSender { get; init; }
    public SettingsRoot SettingsRoot { get; private set; }
    public AlertDataToSend UserDataToSend { get; init; }

    private readonly IServerConnectionChecker _serverConnectionChecker;
    private readonly IPagesPooler _pagesPooler;
    private Dictionary<Pages, Page> _pages = new();
    private readonly Pages _startFormPage = Pages.PhotoPage;

    public App(IServerConnectionChecker serverConnectionChecker, IAlertDataSender dataSender,
        IPagesPooler pagesPooler, AlertDataToSend alertDataToSend)
    {
        InitializeComponent();

        //Currently only light theme available
        UserAppTheme = AppTheme.Light;

        DataSender = dataSender;
        _serverConnectionChecker = serverConnectionChecker;
        _pagesPooler = pagesPooler;
        UserDataToSend = alertDataToSend;

        if (!NoConnectionDisplayer.DisplayIfNoConnection(this, _serverConnectionChecker))
            MainPage = new AppShell();
    }

    public void RefreshPages()
    {
        if (NoConnectionDisplayer.DisplayIfNoConnection(this, _serverConnectionChecker))
            return;

        _pages = new(_pagesPooler.PoolAllPages(this));
        //DisplayPage(_startFormPage);
    }

    public ReadOnlyDictionary<Pages, Page> GetLoadedPages()
        => _pages.AsReadOnly();

    public void DisplayPage(Pages page)
    {
        if (!_pages.ContainsKey(page))
            _pages.Add(page, PageFactory.CreatePage(new PageConfiguration(page, this)));

        //if (_pages[page] is IMustPrepareBeforeDisplay)
        //    ((IMustPrepareBeforeDisplay)_pages[page]).NavigateToResultPageAfterBackgroundDataWasProcessed();

        MainPage = _pages[page];
    }
}
