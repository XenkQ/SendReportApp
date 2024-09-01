using CommunityToolkit.Maui.Views;

namespace MauiApp1.Services;

public interface ILoadingPopupService
{
    void ShowLoadingPopup(string title, string message = "");
    void CloseLoadingPopup();
}

public class LoadingPopupService : ILoadingPopupService
{
    private LoadingPopup _loadingPopup;
    private bool isDisplayed;

    public LoadingPopupService()
    {
        _loadingPopup = new LoadingPopup();
    }

    public void ShowLoadingPopup(string title, string message = "")
    {
        if (!isDisplayed)
        {
            Shell.Current.ShowPopup(_loadingPopup);
            isDisplayed = true;
        }
    }

    public void CloseLoadingPopup()
    {
        if (isDisplayed)
        {
            _loadingPopup.Close();
            isDisplayed = false;
            _loadingPopup = new LoadingPopup();
        }
    }
}
