using MauiApp1.AppPages;
using MauiApp1.GUI.FlowButtons;
using static Android.Graphics.Paint;
using static Android.Provider.MediaStore.Audio;

namespace MauiApp1;

//TODO: Can bind radio buttons value to categories enum
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
