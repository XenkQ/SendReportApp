using MauiApp1.ViewModel.Forms;
using MonkeyFinder;

namespace MauiApp1.View.FormPages;

public partial class DescriptionPage : ContentPage
{
    public DescriptionPage(FormDescriptionViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
	}

    public void OnBackButtonClick(object sender, EventArgs e)
    {
        AppShell.Current.GoToAsync(nameof(PhotoPage), false);
        //_app.DisplayPage(Pages.CategoryPage);
    }

    //public void OnNextButtonClick(object sender, EventArgs e)
    //{
    //    //if (!string.IsNullOrEmpty(DescriptionField.Text))
    //    //    _app.UserDataToSend.Message = DescriptionField.Text;

    //    _app.DisplayPage(Pages.LocalizationPage);
    //}
}