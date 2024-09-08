using MauiApp1.ViewModel.Forms;

namespace MauiApp1.View.FormPages;

public partial class CategoryPage : ContentPage
{
    private readonly FormCategoryViewModel _viewModel;

    public CategoryPage(FormCategoryViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    public void OnCategorySelect(object sender, EventArgs e)
    {
        _viewModel.OnCategorySelect(sender, e);
    }
}