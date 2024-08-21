namespace MauiApp1;

public partial class SendingCompletedPage : ContentPage
{
    private readonly IApp _app;

    public SendingCompletedPage(IApp app)
    {
        InitializeComponent();
        _app = app;
    }

    public void OnExitButtonClick(object sender, EventArgs e)
        => Application.Current.Quit();
}