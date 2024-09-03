using MauiApp1.ViewModel.Forms;
using System.Windows.Input;

namespace MauiApp1.ViewModel;

public abstract class FormBaseViewModel : BaseViewModel
{
    public ICommand ToNextFormCommand { get; }
    public ICommand ToPreviousFormCommand { get; }

    public FormBaseViewModel()
    {
        ToNextFormCommand = new Command(async () => await ToNextFormAsync());
        ToPreviousFormCommand = new Command(async () => await ToPreviousFormAsync());
    }

    protected abstract Task ToNextFormAsync();

    protected virtual async Task ToPreviousFormAsync()
        => await Shell.Current.GoToAsync("..");
}