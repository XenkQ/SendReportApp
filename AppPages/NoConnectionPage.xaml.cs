using MauiApp1.Connection;

namespace MauiApp1;

internal interface IDisplayConnectionInfo
{
	void DisplayConnectionText(NoConectionStates noConectionStates);
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
		=> _app.LoadAppContent();


    public void OnExitButtonClick(object sender, EventArgs e)
		=> Application.Current.Quit();

	public void DisplayConnectionText(NoConectionStates noConectionStates)
	{
		switch(noConectionStates)
		{
			case NoConectionStates.NoInternetConnection:
				ConnectionInfoLabel.Text = "Brak po³¹czenia z internetem!";
				break;

            case NoConectionStates.NoServerConnection:
                ConnectionInfoLabel.Text = "Brak po³¹czenia z serwerem. Spróbuj ponownie póŸniej.";
                break;
        }
	}
}