using MauiApp1.AppPages;
using MauiApp1.Data.Storing;

namespace MauiApp1
{
    public interface IApp
    {
        IEnumerable<Task> GetTasks();
        void AddTask(Task task);
        ISendDataHoldable UserDataToSend { get; }
        void LoadPage(Pages page);
        void LoadAppContent();
    }
}