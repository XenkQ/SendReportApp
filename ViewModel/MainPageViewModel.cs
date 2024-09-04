using MauiApp1.Services;
using MauiApp1.View.FormPages;

namespace MauiApp1.ViewModel;

public class MainPageViewModel : FormBaseViewModel
{
    private readonly INoConnectionDisplayer _noConnectionDisplayer;

    public MainPageViewModel(INoConnectionDisplayer noConnectionDisplayer)
    {
        _noConnectionDisplayer = noConnectionDisplayer;
    }

    protected async override Task ToNextFormAsync()
    {
        if (!_noConnectionDisplayer.DisplayIfNoConnection())
           await Shell.Current.GoToAsync(nameof(PhotoPage), false);
    }

    protected async override Task ToPreviousFormAsync()
        => await Task.CompletedTask;
}
