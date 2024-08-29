using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Input;

namespace MauiApp1.ViewModel;

public abstract partial class FormBaseViewModel : ObservableObject
{   
    [ObservableProperty]
    private string _title;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotBusy))]
    private bool _isBusy;

    public bool IsNotBusy => !IsBusy;
    public ICommand UpdateDataToSendCommand { get; }

    public FormBaseViewModel()
    {
        UpdateDataToSendCommand = new Command(UpdateDataToSend);
    }

    protected abstract void UpdateDataToSend();
}