using MauiApp1.AppPages;
using MauiApp1.GUI.ButtonHolders;

namespace MauiApp1;

public partial class UnsuccessfulSendingPage : ContentPage, IExitButtonHolder
{
    private readonly IApp _app;

    public UnsuccessfulSendingPage(IApp app)
    {
        InitializeComponent();
        _app = app;
    }

    public void OnExitButtonClick(object sender, EventArgs e)
        => Application.Current.Quit();

    private void OnTryAgainButtonClick(object sender, EventArgs e)
        => _app.DisplayPage(Pages.ReportSendLoadingPage);
}