using MauiApp1.AppPages;
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

    public App(IServerConnectionChecker serverConnectionChecker, IPagesPooler pagesPooler, ISendDataHoldable userDataToSend,
        IDataSender dataSender)
    {
        InitializeComponent();

        //Currently only light theme available
        UserAppTheme = AppTheme.Light;

        UserDataToSend = userDataToSend;
        _dataSender = dataSender;
        _serverConnectionChecker = serverConnectionChecker;

        _pages = pagesPooler.PoolAllPages(this);

        MainPage = _pages[Pages.PhotoPage];
        //LoadAppContent();
    }

    public void LoadPage(Pages page)
    {
        MainPage = _pages[page];

        if (MainPage is IMustPrepareAfterLoad)
            ((IMustPrepareAfterLoad)MainPage).PrepareAfterLoad();
    }

    public void LoadAppContent()
    {
        bool isConnectedToNetwork = Connectivity.NetworkAccess == NetworkAccess.Internet;
        bool isConnectedToServer = true; /*_serverConnectionChecker.IsConnected(API_ENDPOINT, "/status");*/

        if (isConnectedToNetwork && isConnectedToServer)
        {
            MainPage = _pages[Pages.PhotoPage];
        }
        else
        {
            MainPage = _pages[Pages.NoConnectionPage];

            var connectionTextDisplay = MainPage as IDisplayConnectionInfo;

            if (connectionTextDisplay == null) return;

            if (isConnectedToServer)
                connectionTextDisplay.DisplayConnectionText(Connection.NoConectionStates.NoInternetConnection);
            else
                connectionTextDisplay.DisplayConnectionText(Connection.NoConectionStates.NoServerConnection);
        }
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
