using MauiApp1.Connection;

namespace MauiApp1;

internal interface IDisplayConnectionInfo
{
	void DisplayConnectionText(ConectionStatuses conectionStatus);
}

public partial class NoConnectionPage : ContentPage, IDisplayConnectionInfo
{
    private readonly IApp _app;

    public NoConnectionPage(IApp app)
	{
		InitializeComponent();
		_app = app;
	}

	private void OnReconnectButtonClick(object sender, EventArgs e)
		=> _app.ReloadPages();


    public void OnExitButtonClick(object sender, EventArgs e)
		=> Application.Current.Quit();

	public void DisplayConnectionText(ConectionStatuses conectionStatus)
	{
		switch(conectionStatus)
		{
			case ConectionStatuses.NoInternetConnection:
				ConnectionInfoLabel.Text = "Brak po³¹czenia z internetem!";
				break;

			case ConectionStatuses.NoServerConnection:
            case ConectionStatuses.NoServerAndInternetConnection:
                ConnectionInfoLabel.Text = "Brak po³¹czenia z serwerem. Spróbuj ponownie póŸniej.";
                break;
        }
	}
}