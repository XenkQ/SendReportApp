namespace MauiApp1;

public partial class SendingInfoPage : ContentPage
{
    private readonly IApp _app;

    public SendingInfoPage(IApp app)
    {
		InitializeComponent();
        _app = app;
	}
}