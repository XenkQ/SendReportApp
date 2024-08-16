using MauiApp1.AppPages;
using MauiApp1.Data.Sending;
using MauiApp1.Data.Storing;
using MauiApp1.Data.Waiting;

namespace MauiApp1;

public partial class App : Application, IApp
{
    public ISendDataHoldable UserDataToSend { get; private set; }
    private readonly IPagesCreator _pagesCreator;
    private readonly Pages[] simplePages = [
        Pages.CategoryPage,
        Pages.DescriptionPage,
        Pages.LocalizationPage,
        Pages.SendingCompletedPage
    ];
    private Dictionary<Pages, Page> _pages;
    private readonly IDataSender _dataSender;
    private IWaitForData<string> _base64ImageDataWaiter;

    public App(IPagesCreator pagesCreator, ISendDataHoldable userDataToSend,
        IDataSender dataSender)
    {
        InitializeComponent();

        //Currently only light theme available
        UserAppTheme = AppTheme.Light;

        UserDataToSend = userDataToSend;
        _dataSender = dataSender;
        _pagesCreator = pagesCreator;

        _base64ImageDataWaiter = new DataWaiter<string>(UserDataToSend.Base64Image);

        _pages = _pagesCreator.CreateSimplePages(simplePages, this);
        var photoPage = _pagesCreator.CreateComplexPage(Pages.PhotoPage, this, _base64ImageDataWaiter);
        _pages.Add(photoPage.Key, photoPage.Value);

        MainPage = _pages[Pages.PhotoPage];
    }

    public void LoadPage(Pages page)
    {
        MainPage = _pages[page];

        if (MainPage is IMustPrepareAfterLoad)
            ((IMustPrepareAfterLoad)MainPage).PrepareAfterLoad();
    }
}
