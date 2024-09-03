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
        UpdateAlertData(DescriptionText);

        await Shell.Current.GoToAsync(nameof(LocalizationPage));
    }

    public void UpdateAlertData(string input)
    {
        if (!string.IsNullOrEmpty(input))
            _alertDataToSend.Message = input;
    }
}
