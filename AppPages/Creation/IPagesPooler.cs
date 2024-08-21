using System.Collections.ObjectModel;

namespace MauiApp1.AppPages.Creation;

public interface IPagesPooler
{
    ReadOnlyDictionary<Pages, Page> PoolAllPages(IApp app);
}