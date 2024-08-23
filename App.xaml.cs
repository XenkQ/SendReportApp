using MauiApp1.AppPages;
using MauiApp1.AppPages.Creation;
using MauiApp1.Connection;
using MauiApp1.Data.Sending;
using MauiApp1.Data.Storing;
using MauiApp1.DTOs;
using System.Collections.ObjectModel;
using System.Linq.Expressions;

namespace MauiApp1;

public interface IApp
{
    IAlertDataToSend UserDataToSend { get; init; }
    IDataSender DataSender { get; init; }
    SettingsRoot SettingsRoot { get; }
    ReadOnlyDictionary<Pages, Page> GetLoadedPages();
    void DisplayPage(Pages page);
    void ReloadPages();
}

public partial class App : Application, IApp
{
    public IAlertDataToSend UserDataToSend { get; init; }
    public IDataSender DataSender { get; init; }
    public SettingsRoot SettingsRoot { get; private set; }

    private readonly IServerConnectionChecker _serverConnectionChecker;
    private readonly IPagesPooler _pagesPooler;
    private Dictionary<Pages, Page> _pages = new ();
    private readonly Pages _startFormPage = Pages.PhotoPage;

    public App(IServerConnectionChecker serverConnectionChecker, IPagesPooler pagesPooler,
        IAlertDataToSend userDataToSend, IDataSender dataSender)
    {
        InitializeComponent();

        //Currently only light theme available
        UserAppTheme = AppTheme.Light;

        UserDataToSend = userDataToSend;
        DataSender = dataSender;
        _serverConnectionChecker = serverConnectionChecker;
        _pagesPooler = pagesPooler;

        ReloadPages();
    }

    public void ReloadPages()
    {
        if (!NoConnectionDisplayer.DisplayIfNoConnection(this, _serverConnectionChecker))
        {
            _pages = new(_pagesPooler.PoolAllPages(this));
            DisplayPage(_startFormPage);
        }
    }

    public ReadOnlyDictionary<Pages, Page> GetLoadedPages()
        => _pages.AsReadOnly();

    public void DisplayPage(Pages page)
    {
        if (!_pages.ContainsKey(page))
            _pages.Add(page, PageFactory.CreatePage(new PageConfiguration(page, this)));

        if (_pages[page] is IMustPrepareBeforeDisplay)
            ((IMustPrepareBeforeDisplay)_pages[page]).Prepare();

        MainPage = _pages[page];
    }
}
