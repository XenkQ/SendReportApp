using MauiApp1.Resources.Languages;
using MauiApp1.ViewModel.Status;

namespace MauiApp1.View.StatusPages;

public partial class SendingResultPage : ContentPage
{
    private readonly SendingResultViewModel _sendingResultViewModel;

    public SendingResultPage(SendingResultViewModel sendingResultViewModel)
	{
		InitializeComponent();
        _sendingResultViewModel = sendingResultViewModel;
        BindingContext = _sendingResultViewModel;
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        if (_sendingResultViewModel.Success)
            DisplaySuccessfulContent();
        else
            DisplayUnsuccessfulContent();
    }

    private void DisplaySuccessfulContent()
    {
        TryAgainBtn.IsVisible = false;
        TitleInfo.Text = AppResources.ReportSended;
        Graphic.Source = "check_circle.png";
    }

    private void DisplayUnsuccessfulContent()
    {
        TryAgainBtn.IsVisible = true;
        TitleInfo.Text = AppResources.ReportNotSended;
        Graphic.Source = "x_circle.png";
    }
}