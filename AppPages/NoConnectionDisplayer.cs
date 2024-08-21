using MauiApp1.Connection;

namespace MauiApp1.AppPages
{
    internal static class NoConnectionDisplayer
    {
        public static bool DisplayIfNoConnection(IApp app)
        {
            switch (ConnectionStatus.Get())
            {
                case ConectionStatuses.NoInternetConnection:
                    DisplayNoConnectionPage(app, ConectionStatuses.NoInternetConnection);
                    return true;

                case ConectionStatuses.NoServerConnection:
                    DisplayNoConnectionPage(app, ConectionStatuses.NoServerConnection);
                    return true;
            }

            return false;
        }

        private static void DisplayNoConnectionPage(IApp app, ConectionStatuses conectionStatus)
        {
            app.DisplayPage(Pages.NoConnectionPage);

            var connectionTextDisplay = app.GetLoadedPages()[Pages.NoConnectionPage] as IDisplayConnectionInfo;

            if (connectionTextDisplay == null) return;

            connectionTextDisplay.DisplayConnectionText(conectionStatus);
        }
    }
}
