using MauiApp1.AppPages;

namespace MauiApp1;

public partial class FormSendLoadingPage : ContentPage, IMustPrepareAfterLoad
{
	private readonly IApp _app;

	public FormSendLoadingPage(IApp app)
	{
		InitializeComponent();
		_app = app;
	}

    public void PrepareAfterLoad()
    {
        Task.Run(() => Task.WaitAll(GetAllTasksFromForms()))
        .ContinueWith(_ => DisplaySendingResultPage());
    }

    private Task[] GetAllTasksFromForms()
        => PagesTasker.GetTasksFromPages(_app.GetLoadedPages().Values).ToArray();

    private bool DisplaySendingResultPage()
        => Application.Current.Dispatcher.Dispatch(() => _app.DisplayPage(Pages.SendingCompletedPage));
}