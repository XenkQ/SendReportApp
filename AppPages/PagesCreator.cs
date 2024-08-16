namespace MauiApp1.AppPages;

/*
 * In this app in terms of simple pages I mean pages with only one constructor argument that is IApp.
 * On the other hand complex pages are pages that have other arguments then IApp.
 */

internal class PagesCreator : IPagesCreator
{
    public Dictionary<Pages, Page> CreateSimplePages(IEnumerable<Pages> simplePagesToCreate, IApp app)
    {
        var simplePages = new Dictionary<Pages, Page>();

        foreach (Pages page in simplePagesToCreate)
            simplePages.Add(page, CreatePage(page, app)!);

        return simplePages;
    }

    public KeyValuePair<Pages, Page?> CreateSimplePage(Pages pageToCreate, IApp app)
        => new KeyValuePair<Pages, Page?>(pageToCreate, CreatePage(pageToCreate, app));

    public KeyValuePair<Pages, Page?> CreateComplexPage(Pages pageToCreate, IApp app, params object?[]? args)
        => new KeyValuePair<Pages, Page?>(pageToCreate, CreatePage(pageToCreate, app, args));

    private Page? CreatePage(Pages page, IApp app)
        => (Page)Activator.CreateInstance(page.ToPageType(app)!, app)!;

    private Page? CreatePage(Pages page, IApp app, params object?[]? otherArgs)
    {
        object?[]? args = new object?[] { app }.Concat(otherArgs!).ToArray();
        return (Page)Activator.CreateInstance(page.ToPageType(app)!, args)!;
    }
}
