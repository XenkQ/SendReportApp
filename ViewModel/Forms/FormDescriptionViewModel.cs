using CommunityToolkit.Mvvm.ComponentModel;
using MauiApp1.Model;
using MauiApp1.Scripts.Processors;
using MauiApp1.View.FormPages;

namespace MauiApp1.ViewModel.Forms;

public partial class FormDescriptionViewModel : FormBaseViewModel, IUpdateAlertData<string>
{
    private readonly AlertDataToSend _alertDataToSend;

    [ObservableProperty]
    private string descriptionText;

    public FormDescriptionViewModel(AlertDataToSend alertDataToSend)
    {
        Title = "Opis";
        _alertDataToSend = alertDataToSend;
    }

    protected override async Task ToNextFormAsync()
    {
        UpdateAlertData(descriptionText);

        await Shell.Current.GoToAsync(nameof(LocalizationPage));
    }

    protected override async Task ToPreviousFormAsync()
        => await Shell.Current.GoToAsync(nameof(CategoryPage));

    public void UpdateAlertData(in string input)
    {
        if (!string.IsNullOrEmpty(input))
            _alertDataToSend.Message = input;
    }
}
