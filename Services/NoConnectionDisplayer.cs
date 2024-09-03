using MauiApp1.View.StatusPages;

namespace MauiApp1.Services;

public interface INoConnectionDisplayer
{
    bool DisplayIfNoConnection();
}

public class NoConnectionDisplayer : INoConnectionDisplayer
{
    private readonly IConnectionStatusService _connectionStatusService;

    public NoConnectionDisplayer(IConnectionStatusService connectionStatus)
    {
        _connectionStatusService = connectionStatus;
    }

    public bool DisplayIfNoConnection()
    {
        switch (_connectionStatusService.GetCurrentStatus())
        {
            case ConnectionStatuses.NoInternetConnection:
                DisplayNoConnectionPage(ConnectionStatuses.NoInternetConnection);
                return true;

            case ConnectionStatuses.NoServerConnection:
                DisplayNoConnectionPage(ConnectionStatuses.NoServerConnection);
                return true;

            case ConnectionStatuses.NoServerAndInternetConnection:
                DisplayNoConnectionPage(ConnectionStatuses.NoServerAndInternetConnection);
                return true;
        }

        return false;
    }

    private static void DisplayNoConnectionPage(ConnectionStatuses status)
        => Shell.Current.GoToAsync(nameof(NoConnectionPage), false, new Dictionary<string, object>
        {
            { "connection_status", status}
        });
}