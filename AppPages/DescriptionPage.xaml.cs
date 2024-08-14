using MauiApp1.AppPages;
using MauiApp1.GUI.FlowButtons;

namespace MauiApp1;

public partial class DescriptionPage : ContentPage, IFlowButtonHolder
{
    private readonly IApp _app;

    public DescriptionPage(IApp app)
	{
		InitializeComponent();
        _app = app;
	}

    public void OnBackButtonClick(object sender, EventArgs e)
    {
        _app.LoadPage(Pages.CategoryPage);
    }

    public void OnNextButtonClick(object sender, EventArgs e)
    {
        if (DescriptionField.Text != string.Empty)
            _app.UserDataToSend.Message = DescriptionField.Text;

        _app.LoadPage(Pages.LocalizationPage);
    }
}