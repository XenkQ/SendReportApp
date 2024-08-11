using MauiApp1.AppPages;
using MauiApp1.Data.Storing;

namespace MauiApp1
{
    public interface IApp
    {
        ISendDataHoldable UserDataToSend { get; }
        void LoadPage(Pages page);
    }
}