using MauiApp1.ViewModel.Status;

namespace MauiApp1.View.StatusPages;

public partial class ReportSendLoadingPage : ContentPage
{
    private readonly ReportSendLoadingViewModel _viewModel;

    public ReportSendLoadingPage(ReportSendLoadingViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
        _viewModel = viewModel;
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        _viewModel.NavigateToResultPageAfterBackgroundDataWasProcessed();
    }
}