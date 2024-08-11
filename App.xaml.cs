using MauiApp1.AppPages;
using MauiApp1.Data.Sending;
using MauiApp1.Data.Storing;
using System.Collections.ObjectModel;

namespace MauiApp1;

public partial class App : Application, IApp
{
    private ReadOnlyDictionary<Pages, Page> _pages;
    public ISendDataHoldable UserDataToSend { get; private set; }
    private readonly IDataSender _dataSender;
    private readonly IStartPageCreator _startPageCreator;

    public App(IStartPageCreator startPageCreator, ISendDataHoldable userDataToSend,
        IDataSender dataSender)
    {
        InitializeComponent();
        UserDataToSend = userDataToSend;
        _dataSender = dataSender;
        _startPageCreator = startPageCreator;

        _pages = _startPageCreator.CreatePagesOnStart(this);
        MainPage = _pages[Pages.PhotoPage];
    }

    public void LoadPage(Pages page)
    {
        MainPage = _pages[page];

        if (MainPage is IMustPrepareAfterLoad)
            ((IMustPrepareAfterLoad)MainPage).PrepareAfterLoad();
    }
}
