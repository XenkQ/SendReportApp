using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp1.View.StatusPages;
using MauiApp1.ViewModel.Forms;

namespace MauiApp1.ViewModel.Status;

[QueryProperty("Success", "success")]
public partial class SendingResultViewModel : BaseViewModel
{
    [ObservableProperty]
    private bool _success;

    [RelayCommand]
    public void Exit()
        => Application.Current.Quit();

    [RelayCommand]
    private void TrySendingAgain()
        => Shell.Current.GoToAsync(nameof(ReportSendLoadingPage));
}
