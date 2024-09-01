using MauiApp1.ViewModel.Forms;

namespace MauiApp1.View.FormPages;

public partial class PhotoPage : ContentPage
{
    public PhotoPage(FormPhotoViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}