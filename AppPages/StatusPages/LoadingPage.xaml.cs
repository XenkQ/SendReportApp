using MauiApp1.AppPages;

namespace MauiApp1;

public partial class FormSendLoadingPage : ContentPage, IMustPrepareBeforeDisplay
{
	private readonly IApp _app;

	public FormSendLoadingPage(IApp app)
	{
		InitializeComponent();
		_app = app;
	}

    public void Prepare()
    {
        Task.Run(() => Task.WaitAll(GetAllTasksFromForms()))
        .ContinueWith(_ => _app.DataSender.SendDataAsync(_app.UserDataToSend))
        .ContinueWith(_ => DisplaySendingResultPage());
    }

    private Task[] GetAllTasksFromForms()
        => PagesTasker.GetTasksFromPages(_app.GetLoadedPages().Values).ToArray();

    private bool DisplaySendingResultPage()
        => Application.Current.Dispatcher.Dispatch(() => _app.DisplayPage(Pages.SendingCompletedPage));
}