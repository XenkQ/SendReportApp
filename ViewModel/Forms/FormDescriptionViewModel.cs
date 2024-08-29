using CommunityToolkit.Mvvm.ComponentModel;
using MauiApp1.Model;

namespace MauiApp1.ViewModel.Forms;

public partial class FormDescriptionViewModel : FormBaseViewModel
{
    [ObservableProperty]
    private string descriptionText;

    private AlertDataToSend _alertDataToSend;

    public FormDescriptionViewModel(AlertDataToSend alertDataToSend)
    {
        Title = "Opis";
        _alertDataToSend = alertDataToSend;
    }

    protected override void UpdateDataToSend()
    {
        if (!string.IsNullOrEmpty(descriptionText))
            _alertDataToSend.Message = descriptionText;
    }
}
