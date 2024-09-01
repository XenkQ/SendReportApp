namespace MauiApp1.Services;

public interface IDialogService
{
    Task ShowAlertAsync(string title, string message, string cancel);
}

class DialogService : IDialogService
{
    public async Task ShowAlertAsync(string title, string message, string cancel)
    {
        var currentPage = Application.Current.MainPage;

        if (currentPage is not null)
            await currentPage.DisplayAlert(title, message, cancel);
    }
}
