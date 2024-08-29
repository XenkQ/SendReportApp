using CommunityToolkit.Mvvm.ComponentModel;

namespace MauiApp1.ViewModel;

public partial class FormBaseViewModel : ObservableObject
{   
    [ObservableProperty]
    private string title;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotBusy))]
    private bool isBusy;

    public bool IsNotBusy => !IsBusy;
}