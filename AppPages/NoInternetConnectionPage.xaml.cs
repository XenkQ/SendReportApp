namespace MauiApp1;

public partial class NoInternetConnectionPage : ContentPage
{
    private readonly IApp _app;

    public NoInternetConnectionPage(IApp app)
	{
		InitializeComponent();
		_app = app;
	}

	private void OnReconnectButtonClick(object sender, EventArgs e)
		=> _app.LoadAppContent();


    public void OnExitButtonClick(object sender, EventArgs e)
		=> Application.Current.Quit();
}