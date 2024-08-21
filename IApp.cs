using MauiApp1.AppPages;
using MauiApp1.Data.Storing;

namespace MauiApp1
{
    public interface IApp
    {
        IEnumerable<Task> GetPagesTasks();
        ISendDataHoldable UserDataToSend { get; }
        void DisplayPage(Pages page);
        void LoadAllPagesIfConnection();
    }
}