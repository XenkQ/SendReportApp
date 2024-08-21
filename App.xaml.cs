using MauiApp1.AppPages;
using MauiApp1.AppPages.Creation;
using MauiApp1.Data.Processors;
using MauiApp1.Data.Sending;
using MauiApp1.Data.Storing;
using System.Collections.ObjectModel;

namespace MauiApp1;

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

        LoadAllPagesIfConnection();
        DisplayPage(_startFormPage);
    }

    public ReadOnlyDictionary<Pages, Page> GetLoadedPages()
        => _pages.AsReadOnly();

    public void DisplayPage(Pages page)
    {
        if(!_pages.ContainsKey(page))
            throw new ArgumentException($"Can't load {page} becouse this page is not pooled");

        if (_pages[page] is IMustPrepareAfterLoad)
            ((IMustPrepareAfterLoad)_pages[page]).PrepareAfterLoad();

        MainPage = _pages[page];
    }

    public void LoadAllPagesIfConnection()
    {
        bool isConnectedToNetwork = Connectivity.NetworkAccess == NetworkAccess.Internet;
        bool isConnectedToServer = true; /*_serverConnectionChecker.IsConnected(API_ENDPOINT, "/status");*/

        if (isConnectedToNetwork && isConnectedToServer)
            _pages = new (_pagesPooler.PoolAllPages(this));
        else if(isConnectedToNetwork && !isConnectedToServer)
            DisplayNoConnectionPage(Connection.NoConectionAnnouncements.NoServerConnection);
        else if(!isConnectedToNetwork && isConnectedToServer)
            DisplayNoConnectionPage(Connection.NoConectionAnnouncements.NoInternetConnection);
    }

    private void DisplayNoConnectionPage(Connection.NoConectionAnnouncements announcement)
    {
        if(_pages.ContainsKey(Pages.NoConnectionPage))
            PageFactory.CreatePage(new PageConfiguration(Pages.NoConnectionPage, this));

        DisplayPage(Pages.NoConnectionPage);

        var connectionTextDisplay = _pages[Pages.NoConnectionPage] as IDisplayConnectionInfo;

        if (connectionTextDisplay == null) return;
        
        connectionTextDisplay.DisplayConnectionText(announcement);
    }
}
