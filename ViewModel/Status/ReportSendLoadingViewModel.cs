using MauiApp1.Model;
using MauiApp1.Services;
using MauiApp1.View.StatusPages;
using MauiApp1.ViewModel.Forms;

namespace MauiApp1.ViewModel.Status;

public class ReportSendLoadingViewModel : BaseViewModel
{
    private readonly AlertDataToSend _alertDataToSend;
    private readonly IAlertDataSender _dataSender;
    private readonly IFormBackgroundTaskObserver _formBackgroundTaskObserver;

    public ReportSendLoadingViewModel(AlertDataToSend alertDataToSend, IAlertDataSender dataSender,
        IFormBackgroundTaskObserver formBackgroundTaskObserver)
    {
        _alertDataToSend = alertDataToSend;
        _dataSender = dataSender;
        _formBackgroundTaskObserver = formBackgroundTaskObserver;
    }

    public void NavigateToResultPageAfterBackgroundDataWasProcessed()
    {
        Task.Run(() => Task.WaitAll(_formBackgroundTaskObserver.GetAllTasksFromObservedDataProcessors().ToArray()))
            .ContinueWith(_ => _dataSender.SendDataAsync(_alertDataToSend))
            .ContinueWith(task => DisplaySendingResultPage(task.Result.Result));
    }

    private void DisplaySendingResultPage(HttpResponseMessage sendingRequestMessage)
    {
        Application.Current.Dispatcher
            .Dispatch(() =>
                Shell.Current.GoToAsync($"{nameof(SendingResultPage)}?success={sendingRequestMessage.IsSuccessStatusCode}")
            );
    }
}
