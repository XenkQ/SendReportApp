using MauiApp1.View.FormPages;

namespace MonkeyFinder;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(PhotoPage), typeof(PhotoPage));
		Routing.RegisterRoute(nameof(DescriptionPage), typeof(DescriptionPage));
	}
}