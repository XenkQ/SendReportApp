using MauiApp1.Scripts.GUI.ButtonHolders;

namespace MauiApp1.View.StatusPages;

public partial class SuccessfulSendingPage : ContentPage, IExitButtonHolder
{
    private readonly IApp _app;

    public SuccessfulSendingPage(IApp app)
    {
        InitializeComponent();
        _app = app;
    }

    public void OnExitButtonClick(object sender, EventArgs e)
        => Application.Current.Quit();
}