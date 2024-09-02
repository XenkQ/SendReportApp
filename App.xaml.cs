using MonkeyFinder;

namespace MauiApp1;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        //Currently only light theme available
        UserAppTheme = AppTheme.Light;
        
        MainPage = new AppShell();
    }
}
