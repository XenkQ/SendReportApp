using System.Collections.ObjectModel;

namespace MauiApp1.AppPages;

public interface IPagesPooler
{
    ReadOnlyDictionary<Pages, Page> PoolAllPages(IApp app);
}