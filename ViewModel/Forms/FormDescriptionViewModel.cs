using CommunityToolkit.Mvvm.ComponentModel;
using MauiApp1.Model;
using System.Windows.Input;

namespace MauiApp1.ViewModel.Forms;

public partial class FormDescriptionViewModel : FormBaseViewModel, IFormViewModel
{
    [ObservableProperty]
    private string descriptionText;

    public ICommand UpdateDataToSendCommand { get; }
    private AlertDataToSend _alertDataToSend;

    public FormDescriptionViewModel(AlertDataToSend alertDataToSend)
    {
        Title = "Opis";
        _alertDataToSend = alertDataToSend;
        UpdateDataToSendCommand = new Command(() => UpdateDataToSend());
    }

    public void UpdateDataToSend()
    {
        if (!string.IsNullOrEmpty(descriptionText))
            _alertDataToSend.Message = descriptionText;
    }
}
