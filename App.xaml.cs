using MauiApp1.AppPages;
using MauiApp1.Data.Processors;
using MauiApp1.Data.Sending;
using MauiApp1.Data.Storing;
using System.Collections.ObjectModel;

namespace MauiApp1;

public partial class App : Application, IApp
{
    public ISendDataHoldable UserDataToSend { get; private set; }
    private ReadOnlyDictionary<Pages, Page> _pages;
    private readonly IDataSender _dataSender;

    public App(IPagesPooler pagesPooler, ISendDataHoldable userDataToSend,
        IDataSender dataSender)
    {
        InitializeComponent();

        //Currently only light theme available
        UserAppTheme = AppTheme.Light;

        UserDataToSend = userDataToSend;
        _dataSender = dataSender;

        _pages = pagesPooler.PoolAllPages(this);

        LoadAppContent();
    }

    public void LoadPage(Pages page)
    {
        MainPage = _pages[page];

        if (MainPage is IMustPrepareAfterLoad)
            ((IMustPrepareAfterLoad)MainPage).PrepareAfterLoad();
    }

    public void LoadAppContent()
    {
        if (Connectivity.NetworkAccess == NetworkAccess.Internet)
        {
            MainPage = _pages[Pages.PhotoPage];
        }
        else
        {
            MainPage = _pages[Pages.NoInternetConnectionPage];
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
