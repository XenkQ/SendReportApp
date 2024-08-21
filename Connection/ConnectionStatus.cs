namespace MauiApp1.Connection;

public enum ConectionStatuses
{
    NoInternetConnection,
    NoServerConnection,
    NoServerAndInternetConnection,
    Connected
}

internal static class ConnectionStatus
{
    public static ConectionStatuses Get()
    {
        bool isConnectedToNetwork = Connectivity.NetworkAccess == NetworkAccess.Internet;
        bool isConnectedToServer = true; /*_serverConnectionChecker.IsConnected(API_ENDPOINT, "/status");*/

        if (isConnectedToNetwork && !isConnectedToServer)
            return ConectionStatuses.NoServerConnection;
        else if (!isConnectedToNetwork && isConnectedToServer)
            return ConectionStatuses.NoInternetConnection;
        else if(isConnectedToNetwork && !isConnectedToServer)
            return ConectionStatuses.NoServerAndInternetConnection;
        else
            return ConectionStatuses.Connected;
    }
}
