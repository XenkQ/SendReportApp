using System.Text.Json.Serialization;

namespace MauiApp1.Model.Categories;

public record Category(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("icon")] string Icon,
    [property: JsonPropertyName("name")] string Name
);

public record CategoryGroup(
    [property: JsonPropertyName("group")] string Group,
    [property: JsonPropertyName("categories")] IReadOnlyList<Category> Categories
);

public record CategoriesRoot(
    [property: JsonPropertyName("category_groups")] IReadOnlyList<CategoryGroup> CategoryGroups
);