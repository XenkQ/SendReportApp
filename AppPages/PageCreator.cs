using System.Collections.ObjectModel;
using System.Reflection;

namespace MauiApp1.AppPages;

internal class PageCreator : IStartPageCreator
{
    public ReadOnlyDictionary<Pages, Page> CreatePagesOnStart(IApp app)
    {
        var pages = new Dictionary<Pages, Page>();

        foreach (Pages page in Enum.GetValues(typeof(Pages)))
            pages.Add(page, CreatePage(page, app)!);

        return pages.AsReadOnly();
    }

    private Page? CreatePage(Pages page, IApp app)
    {
        var pageTypeFullName = app.GetType().Namespace + $".{page}";
        var pageType = Assembly.GetExecutingAssembly().GetType(pageTypeFullName);

        if (pageType == null)
            throw new NullReferenceException($"Page type {pageType} is not exisitng");

        return (Page)Activator.CreateInstance(pageType, app)!;
    }
}
