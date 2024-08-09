using MauiApp1.Scripts;

namespace MauiApp1
{
    public interface IApp
    {
        void LoadPage(Pages page);
        ISendDataHoldable UserDataToSend { get; }
    }
}