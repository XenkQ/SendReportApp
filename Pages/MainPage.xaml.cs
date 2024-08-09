using MauiApp1.Scripts;

namespace MauiApp1;

public partial class MainPage : ContentPage, IFlowNextButtonHolder
{
    private readonly IApp _app;

    public MainPage(IApp app)
    {
        InitializeComponent();
        _app = app;
    }

    public void OnNextButtonClick(object sender, EventArgs e)
    {
        _app.LoadPage(Pages.PhotoPage);
    }
}
