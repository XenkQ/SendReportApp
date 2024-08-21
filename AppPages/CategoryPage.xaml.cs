using MauiApp1.AppPages;
using MauiApp1.GUI.FlowButtons;

namespace MauiApp1;

//TODO: Can bind radio buttons value to categories enum
public partial class CategoryPage : ContentPage, IFlowButtonHolder
{
    private readonly IApp _app;
    private Categories? _userCategorChoice;

    public CategoryPage(IApp app)
    {
        InitializeComponent();
        _app = app;
    }

    public void OnBackButtonClick(object sender, EventArgs e)
    {
        
        _app.DisplayPage(Pages.PhotoPage);
    }

    public async void OnNextButtonClick(object sender, EventArgs e)
    {
        if(_userCategorChoice != null)
        {
            _app.UserDataToSend.Category = (int)_userCategorChoice;
            _app.DisplayPage(Pages.DescriptionPage);
        }
        else
        {
            await DisplayAlert("Nie wybrano kategorii!", "Wybierz kategorię najlepiej pasującą do zgłoszenia.", "OK");
        }
    }

    private void OnCategoryChange(object sender, EventArgs e)
    {
        var radioButton = sender as RadioButton;

        if (radioButton == null) 
            throw new ArgumentException("Sender is not of object type");

        int categoryAsInt = int.Parse(radioButton.Value.ToString()!);

        _userCategorChoice = (Categories)categoryAsInt;
    }
}
