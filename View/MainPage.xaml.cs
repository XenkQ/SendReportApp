using MauiApp1.View.FormPages;

namespace MauiApp1.View;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();

		Shell.Current.GoToAsync(nameof(PhotoPage), false);
	}
}