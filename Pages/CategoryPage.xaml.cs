using MauiApp1.Scripts;

namespace MauiApp1;

public partial class CategoryPage : ContentPage, IFlowButtonHolder
{
    private readonly IApp _app;

    public CategoryPage(IApp app)
    {
        InitializeComponent();
        _app = app;
    }

    public void OnBackButtonClick(object sender, EventArgs e)
    {
        _app.LoadPage(Pages.PhotoPage);
    }

    public void OnNextButtonClick(object sender, EventArgs e)
    {
        _app.LoadPage(Pages.DescriptionPage);
    }
}
