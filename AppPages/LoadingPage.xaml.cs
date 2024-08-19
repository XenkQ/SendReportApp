using MauiApp1.AppPages;

namespace MauiApp1;

public partial class LoadingPage : ContentPage, IMustPrepareAfterLoad
{
	private readonly IApp _app;

	public LoadingPage(IApp app)
	{
		InitializeComponent();
		_app = app;
	}

    public void PrepareAfterLoad()
    {
        Task.Run(() => Task.WaitAll(_app.GetTasks().ToArray()))
		.ContinueWith(_ => Application.Current.Dispatcher.Dispatch(() =>
		_app.LoadPage(Pages.SendingCompletedPage)));
    }
}