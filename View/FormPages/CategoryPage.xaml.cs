using MauiApp1.ViewModel.Forms;

namespace MauiApp1.View.FormPages;

//TODO: Reading radio buttons from api
public partial class CategoryPage : ContentPage
{
    private readonly FormCategoryViewModel _viewModel;

    public CategoryPage(FormCategoryViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    private void OnCategoryChange(object sender, EventArgs e)
    {
        _viewModel.OnCategoryChange(sender, e);
    }
}
