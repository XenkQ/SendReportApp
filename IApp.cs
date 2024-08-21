using MauiApp1.AppPages;
using MauiApp1.Data.Storing;
using System.Collections.ObjectModel;

namespace MauiApp1
{
    public interface IApp
    {
        ISendDataHoldable UserDataToSend { get; }
        ReadOnlyDictionary<Pages, Page> GetLoadedPages();
        void DisplayPage(Pages page);
        void LoadAllPagesIfConnection();
    }
}