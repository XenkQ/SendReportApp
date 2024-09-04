using CommunityToolkit.Mvvm.ComponentModel;
using MauiApp1.Model;
using MauiApp1.Model.Categories;
using MauiApp1.Scripts.Processors;
using MauiApp1.Services;
using MauiApp1.View.FormPages;
using System.Reflection;
using System.Text.Json;

namespace MauiApp1.ViewModel.Forms;

public partial class FormCategoryViewModel : FormBaseViewModel,
    IUpdateAlertData<int>
{
    private readonly AlertDataToSend _alertDataToSend;
    private readonly IDialogService _dialogService;

    [ObservableProperty]
    private IReadOnlyList<CategoryGroup> _listOfCategories;

    [ObservableProperty]
    private int _categoryChoiceId = 0;

    public FormCategoryViewModel(AlertDataToSend alertDataToSend, IDialogService dialogService)
    {
        Title = "Kategoria";
        _alertDataToSend = alertDataToSend;
        _dialogService = dialogService;

        var assembly = Assembly.GetExecutingAssembly();
        using var stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.MockData.categories.json");
        //bool exists = File.Exists($"{assembly.GetName().Name}.Categories.json");

        var categoriesRoot = JsonSerializer.Deserialize<CategoriesRoot>(stream);
        _listOfCategories = categoriesRoot.CategoryGroups;
    }

    protected override async Task ToNextFormAsync()
    {
        if (CategoryChoiceId != 0)
        {
            UpdateAlertData(CategoryChoiceId);
            await Shell.Current.GoToAsync(nameof(DescriptionPage));
        }
        else
        {
            await _dialogService.ShowAlertAsync("Nie wybrano kategorii!", "Wybierz kategorię najlepiej pasującą do zgłoszenia.", "OK");
        }
    }

    public void OnCategoryChange(object sender, EventArgs e)
    {
        var radioButton = sender as RadioButton;

        if (radioButton == null)
            throw new ArgumentException("Sender is not of object type");

        CategoryChoiceId = int.Parse(radioButton.Value.ToString()!);
    }

    public void UpdateAlertData(int input)
    {
        _alertDataToSend.Category = input;
    }
}
