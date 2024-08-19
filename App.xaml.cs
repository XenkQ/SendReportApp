using MauiApp1.AppPages;
using MauiApp1.Data.Sending;
using MauiApp1.Data.Storing;

namespace MauiApp1;

public partial class App : Application, IApp
{
    public ISendDataHoldable UserDataToSend { get; private set; }
    private readonly IPagesCreator _pagesCreator;
    private readonly Pages[] simplePages = [
        Pages.PhotoPage,
        Pages.CategoryPage,
        Pages.DescriptionPage,
        Pages.LocalizationPage,
        Pages.LoadingPage,
        Pages.SendingCompletedPage
    ];
    private Dictionary<Pages, Page> _pages;
    private readonly IDataSender _dataSender;
    private List<Task> _tasks = new();

    public App(IPagesCreator pagesCreator, ISendDataHoldable userDataToSend,
        IDataSender dataSender)
    {
        InitializeComponent();

        //Currently only light theme available
        UserAppTheme = AppTheme.Light;

        UserDataToSend = userDataToSend;
        _dataSender = dataSender;
        _pagesCreator = pagesCreator;

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
            _pages = _pagesCreator.CreateSimplePages(simplePages, this);
            MainPage = _pages[Pages.PhotoPage];
        }
        else
        {
            var noInternetConnectionPage = _pagesCreator.CreateSimplePage(Pages.NoInternetConnectionPage, this);
            MainPage = noInternetConnectionPage.Value;
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
