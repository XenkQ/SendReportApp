using MauiApp1.AppPages;

namespace MauiApp1;

public partial class ReportSendLoadingPage : ContentPage, IMustPrepareBeforeDisplay
{
	private readonly IApp _app;

	public ReportSendLoadingPage(IApp app)
	{
		InitializeComponent();
		_app = app;
	}

    public void Prepare()
    {
        Task.Run(() => Task.WaitAll(GetAllTasksFromForms()))
        .ContinueWith(_ => _app.DataSender.SendDataAsync(_app.UserDataToSend))
        .ContinueWith(task => DisplaySendingResultPage(task.Result.Result));
    }

    private Task[] GetAllTasksFromForms()
        => PagesTasker.GetTasksFromPages(_app.GetLoadedPages().Values).ToArray();

    private void DisplaySendingResultPage(HttpResponseMessage sendingRequestMessage)
    {
        var appDispather = Application.Current.Dispatcher;
        if (sendingRequestMessage.IsSuccessStatusCode)
            appDispather.Dispatch(() => _app.DisplayPage(Pages.SuccessfulSendingPage));
        else
            appDispather.Dispatch(() => _app.DisplayPage(Pages.UnsuccessfulSendingPage));
    }
}