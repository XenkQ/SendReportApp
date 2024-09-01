using MauiApp1.ViewModel.Forms;

namespace MauiApp1.View.FormPages;

public partial class DescriptionPage : ContentPage
{
    public DescriptionPage(FormDescriptionViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
	}
}