using MauiApp1.View.FormPages;
using MauiApp1.View.StatusPages;

namespace MonkeyFinder;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(PhotoPage), typeof(PhotoPage));
		Routing.RegisterRoute(nameof(CategoryPage), typeof(CategoryPage));
		Routing.RegisterRoute(nameof(DescriptionPage), typeof(DescriptionPage));
		Routing.RegisterRoute(nameof(LocalizationPage), typeof(LocalizationPage));
		Routing.RegisterRoute(nameof(ReportSendLoadingPage), typeof(ReportSendLoadingPage));
		Routing.RegisterRoute(nameof(SendingResultPage), typeof(SendingResultPage));
	}
}