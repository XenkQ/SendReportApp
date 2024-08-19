using System.Collections.ObjectModel;

namespace MauiApp1.AppPages;

internal class PagesPooler : IPagesPooler
{
    public ReadOnlyDictionary<Pages, Page> PoolAllPages(IApp app)
    {
        var simplePages = new Dictionary<Pages, Page>();

        foreach (Pages page in Enum.GetValues(typeof(Pages)))
            simplePages.Add(page, CreatePage(page, app)!);

        return simplePages.AsReadOnly();
    }

    private Page? CreatePage(Pages page, IApp app)
        => (Page)Activator.CreateInstance(page.ToPageType(app)!, app)!;
}
