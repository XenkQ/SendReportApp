using CommunityToolkit.Mvvm.ComponentModel;

namespace MauiApp1.ViewModel.Forms;

public partial class BaseViewModel : ObservableObject
{
    [ObservableProperty]
    private string _title;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotBusy))]
    private bool _isBusy;

    public bool IsNotBusy => !IsBusy;
}
