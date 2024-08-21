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
    private ReadOnlyDictionary<Pages, Page> _pages;
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

    public void DisplayPage(Pages page)
    {
        if (!_pages.ContainsKey(page))
            throw new NullReferenceException($"{page} is not pooled");

        SetMainPage(_pages[page]);

        if (MainPage is IMustPrepareAfterLoad)
            ((IMustPrepareAfterLoad)MainPage).PrepareAfterLoad();
    }

    public void DisplayPage(Page page)
        => SetMainPage(page);

    private void SetMainPage(Page page)
    {
        MainPage = page;

        if (MainPage is IMustPrepareAfterLoad)
            ((IMustPrepareAfterLoad)MainPage).PrepareAfterLoad();
    }

    public void LoadAllPagesIfConnection()
    {
        bool isConnectedToNetwork = Connectivity.NetworkAccess == NetworkAccess.Internet;
        bool isConnectedToServer = true; /*_serverConnectionChecker.IsConnected(API_ENDPOINT, "/status");*/

        if (isConnectedToNetwork && isConnectedToServer)
            _pages = _pagesPooler.PoolAllPages(this);
        else if(isConnectedToNetwork && !isConnectedToServer)
            DisplayNoConnectionPage(Connection.NoConectionAnnouncements.NoServerConnection);
        else if(!isConnectedToNetwork && isConnectedToServer)
            DisplayNoConnectionPage(Connection.NoConectionAnnouncements.NoInternetConnection);
    }

    private void DisplayNoConnectionPage(Connection.NoConectionAnnouncements announcement)
    {
        var connectionPage = _pages.ContainsKey(Pages.NoConnectionPage) 
            ? _pages[Pages.NoConnectionPage]
            : PageFactory.CreatePage(new PageConfiguration(Pages.NoConnectionPage, this));
        
        if (_pages.ContainsKey(Pages.NoConnectionPage))
            DisplayPage(Pages.NoConnectionPage);
        else
            DisplayPage(connectionPage);

        var connectionTextDisplay = connectionPage as IDisplayConnectionInfo;

        if (connectionTextDisplay == null) return;
        
        connectionTextDisplay.DisplayConnectionText(announcement);
    }

    public IEnumerable<Task> GetPagesTasks()
    {
        var tasks = new List<Task>();
        foreach(var page in _pages)
        {
            var dataProcessor = page.Value as IProcessDataInBackground;
            if(dataProcessor != null)
                tasks.Add(dataProcessor.GetProcessedTask());
        }

        return tasks;
    }
}
