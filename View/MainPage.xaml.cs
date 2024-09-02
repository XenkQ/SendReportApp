using MauiApp1.Services;
using MauiApp1.View.FormPages;

namespace MauiApp1.View;

public partial class MainPage : ContentPage
{
	public MainPage(INoConnectionDisplayer noConnectionDisplayer)
	{
		InitializeComponent();

		if (!noConnectionDisplayer.DisplayIfNoConnection())
			Shell.Current.GoToAsync(nameof(PhotoPage), false);
	}
}