using System.Collections.ObjectModel;
using MauiApp1.AppPages.Creation;

namespace MauiApp1.AppPages;

internal class PagePooler : IPagesPooler
{
    public ReadOnlyDictionary<Pages, Page> PoolAllPages(IApp app)
    {
        var simplePages = new Dictionary<Pages, Page>();

        foreach (Pages page in Enum.GetValues(typeof(Pages)))
            simplePages.Add(page, PageFactory.CreatePage(new PageConfiguration(page, app))!);

        return simplePages.AsReadOnly();
    }
}
