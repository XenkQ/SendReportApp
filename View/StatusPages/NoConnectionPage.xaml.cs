using MauiApp1.ViewModel.Status;

namespace MauiApp1.View.StatusPages;

public partial class NoConnectionPage : ContentPage
{
    private readonly NoConnectionViewModel _viewModel;

    public NoConnectionPage(NoConnectionViewModel viewModel)
	{
		InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
	}
}