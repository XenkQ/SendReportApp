using System.Collections.ObjectModel;
using MauiApp1.Scripts;

namespace MauiApp1.Scripts.Creation;

public interface IPagesPooler
{
    ReadOnlyDictionary<Pages, Page> PoolAllPages(IApp app);
}