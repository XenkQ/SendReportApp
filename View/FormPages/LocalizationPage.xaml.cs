using MauiApp1.ViewModel.Forms;

namespace MauiApp1.View.FormPages;

public partial class LocalizationPage : ContentPage
{
    public LocalizationPage(FormLocalizationViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        //MapContentChangeBehavior.ContentChanged += OnContentChange!;
    }
}