using System.Collections.ObjectModel;

namespace MauiApp1.AppPages;

public interface IStartPageCreator
{
    ReadOnlyDictionary<Pages, Page> CreatePagesOnStart(IApp app);
}