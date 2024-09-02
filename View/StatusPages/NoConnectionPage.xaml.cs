using MauiApp1.Services;
using MauiApp1.ViewModel.Status;

namespace MauiApp1.View.StatusPages;

internal interface IDisplayConnectionInfo
{
	void DisplayConnectionText(ConnectionStatuses conectionStatus);
}

public partial class NoConnectionPage : ContentPage
{
    public NoConnectionPage(NoConnectionViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}