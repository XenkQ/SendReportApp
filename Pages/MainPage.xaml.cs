


using MauiApp1.Scripts;

namespace MauiApp1;

public partial class MainPage : ContentPage, IFlowNextButtonHolder
{
    public MainPage()
    {
        InitializeComponent();
    }

    public void OnNextButtonClick(object sender, EventArgs e)
    {
        ((App)Application.Current!).LoadPage(Pages.PhotoPage);
    }
}
