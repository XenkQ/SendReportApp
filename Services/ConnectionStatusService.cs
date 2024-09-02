using MauiApp1.Scripts.Connection;

namespace MauiApp1.Services;

public enum ConnectionStatuses
{
    NoInternetConnection,
    NoServerConnection,
    NoServerAndInternetConnection,
    Connected
}

public interface IConnectionStatusService
{
    ConnectionStatuses GetCurrentStatus();
}

public class ConnectionStatusService : IConnectionStatusService
{
    private readonly IServerConnectionChecker _serverConnectionChecker;

    public ConnectionStatusService(IServerConnectionChecker serverConnectionChecker)
    {
        _serverConnectionChecker = serverConnectionChecker;
    }

    public ConnectionStatuses GetCurrentStatus()
    {
        bool isConnectedToNetwork = Connectivity.NetworkAccess == NetworkAccess.Internet;
        bool isConnectedToServer = _serverConnectionChecker.IsConnected();

        if (isConnectedToNetwork && !isConnectedToServer)
            return ConnectionStatuses.NoServerConnection;
        else if (!isConnectedToNetwork && isConnectedToServer)
            return ConnectionStatuses.NoInternetConnection;
        else if (!isConnectedToNetwork && !isConnectedToServer)
            return ConnectionStatuses.NoServerAndInternetConnection;
        else
            return ConnectionStatuses.Connected;
    }
}
