using System.Collections.ObjectModel;

namespace MauiApp1.AppPages;

public interface IPagesCreator
{
    Dictionary<Pages, Page> CreateSimplePages(IEnumerable<Pages> pagesToCreate, IApp app);
    KeyValuePair<Pages, Page> CreateComplexPage(Pages pageToCreate, IApp app, params object?[]? args);
}