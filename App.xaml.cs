using MauiApp1.AppPages;
using MauiApp1.AppPages.Creation;
using MauiApp1.Connection;
using MauiApp1.Data.Sending;
using MauiApp1.Data.Storing;
using MauiApp1.DTOs;
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
using System.Collections.ObjectModel;

namespace MauiApp1;

public interface IApp
{
    IDataSender DataSender { get; init; }
    SettingsRoot SettingsRoot { get; }
    AlertDataToSend UserDataToSend { get; init; }
    ReadOnlyDictionary<Pages, Page> GetLoadedPages();
    void DisplayPage(Pages page);
    void RefreshPages();
}

public partial class App : Application, IApp
{
    public IDataSender DataSender { get; init; }
    public SettingsRoot SettingsRoot { get; private set; }
    public AlertDataToSend UserDataToSend { get; init; }

    private readonly IServerConnectionChecker _serverConnectionChecker;
    private readonly IPagesPooler _pagesPooler;
    private Dictionary<Pages, Page> _pages = new();
    private readonly Pages _startFormPage = Pages.PhotoPage;

    public App(IServerConnectionChecker serverConnectionChecker, IDataSender dataSender,
        IPagesPooler pagesPooler, AlertDataToSend alertDataToSend)
    {
        InitializeComponent();

        //Currently only light theme available
        UserAppTheme = AppTheme.Light;

        DataSender = dataSender;
        _serverConnectionChecker = serverConnectionChecker;
        _pagesPooler = pagesPooler;
        UserDataToSend = alertDataToSend;

        RefreshPages();
    }

    public void RefreshPages()
    {
        if (NoConnectionDisplayer.DisplayIfNoConnection(this, _serverConnectionChecker))
            return;
            
        _pages = new(_pagesPooler.PoolAllPages(this));
        DisplayPage(_startFormPage);
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
