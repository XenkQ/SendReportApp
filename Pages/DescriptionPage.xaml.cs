using MauiApp1.Scripts;

namespace MauiApp1;

public partial class DescriptionPage : ContentPage, IFlowBackButtonsHolder
{
    private readonly IApp _app;
    public DescriptionPage(IApp app)
	{
		InitializeComponent();
        _app = app;
	}

    public void OnBackButtonClick(object sender, EventArgs e)
    {
        _app.LoadPage(Pages.PhotoPage);
    }

    private void OnSendButtonClick(object sender, EventArgs e)
    {
        
    }
}