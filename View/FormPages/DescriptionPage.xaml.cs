using MauiApp1.AppPages;
using MauiApp1.GUI.FlowButtons;

namespace MauiApp1.View.FormPages;

public partial class DescriptionPage : ContentPage
{
    private readonly IApp _app;

    public DescriptionPage(IApp app)
	{
		InitializeComponent();
        _app = app;
	}

    public void OnBackButtonClick(object sender, EventArgs e)
    {
        _app.DisplayPage(Pages.CategoryPage);
    }

    //public void OnNextButtonClick(object sender, EventArgs e)
    //{
    //    //if (!string.IsNullOrEmpty(DescriptionField.Text))
    //    //    _app.UserDataToSend.Message = DescriptionField.Text;

    //    _app.DisplayPage(Pages.LocalizationPage);
    //}
}