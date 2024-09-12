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
    private ConnectionStatuses _connectionStatus;

    [ObservableProperty]
    private string _connectionInfo;

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

    partial void OnConnectionStatusChanged(ConnectionStatuses value)
    {
        switch (ConnectionStatus)
        {
            case ConnectionStatuses.NoInternetConnection:
                ConnectionInfo = "Brak połączenia z internetem!";
                break;

            case ConnectionStatuses.NoServerConnection:
            case ConnectionStatuses.NoServerAndInternetConnection:
                ConnectionInfo = "Brak połączenia z serwerem! Spróbuj ponownie później.";
                break;
        }
    }
}
