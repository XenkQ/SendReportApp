
namespace MauiApp1;

public partial class DescriptionPage : ContentPage, IFlowBackButtonsHolder
{
	public DescriptionPage()
	{
		InitializeComponent();
	}

    public void OnBackButtonClick(object sender, EventArgs e)
    {
        ((App)Application.Current).LoadPage(Pages.PhotoPage);
    }

    private void OnSendButtonClick(object sender, EventArgs e)
    {

    }
}