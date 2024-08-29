using System.Windows.Input;

namespace MauiApp1.ViewModel.Forms;

public interface IFormViewModel
{
    public ICommand UpdateDataToSendCommand { get; }
    public void UpdateDataToSend();
}