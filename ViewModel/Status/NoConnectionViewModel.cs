using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp1.Services;
using MauiApp1.View.FormPages;
using MauiApp1.ViewModel.Forms;

namespace MauiApp1.ViewModel.Status;

[QueryProperty("ConnectionStatus", "connection_status")]
public partial class NoConnectionViewModel : BaseViewModel
{
    private readonly IConnectionStatusService _connectionStatusService;

    [ObservableProperty]
    private string _connectionStatus;

    public NoConnectionViewModel(IConnectionStatusService connectionStatusService)
    {
        _connectionStatusService = connectionStatusService;
    }

    [RelayCommand]
    private void TryReconnect()
    {
        //TODO: Change to try reload last known site
        if (_connectionStatusService.GetCurrentStatus() == ConnectionStatuses.Connected)
            Shell.Current.GoToAsync(nameof(PhotoPage));
    }

    [RelayCommand]
    public void Exit() => Application.Current.Quit();

    public void SetConnectionInfo(ConnectionStatuses conectionStatus)
    {
        switch (conectionStatus)
        {
            case ConnectionStatuses.NoInternetConnection:
                ConnectionStatus = "Brak połączenia z internetem!";
                break;

            case ConnectionStatuses.NoServerConnection:
            case ConnectionStatuses.NoServerAndInternetConnection:
                ConnectionStatus = "Brak połączenia z serwerem. Spróbuj ponownie później.";
                break;
        }
    }
}
