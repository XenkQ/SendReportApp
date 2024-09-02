﻿using CommunityToolkit.Mvvm.ComponentModel;
using MauiApp1.Model;
using MauiApp1.Scripts.Processors;
using MauiApp1.Services;
using MauiApp1.View.FormPages;

namespace MauiApp1.ViewModel.Forms;

public partial class FormCategoryViewModel : FormBaseViewModel,
    IUpdateAlertData<int>
{
    private readonly AlertDataToSend _alertDataToSend;
    private readonly DialogService _dialogService;

    [ObservableProperty]
    private Categories _userCategorChoice;

    public FormCategoryViewModel(AlertDataToSend alertDataToSend, IDialogService dialogService)
    {
        _alertDataToSend = alertDataToSend;
    }

    protected override async Task ToNextFormAsync()
    {
        if (UserCategorChoice != default)
        {
            UpdateAlertData((int)UserCategorChoice);
            await Shell.Current.GoToAsync(nameof(DescriptionPage));
        }
        else
        {
            await _dialogService.ShowAlertAsync("Nie wybrano kategorii!", "Wybierz kategorię najlepiej pasującą do zgłoszenia.", "OK");
        }
    }

    protected override async Task ToPreviousFormAsync()
    {
        await Shell.Current.GoToAsync(nameof(PhotoPage));
    }

    public void OnCategoryChange(object sender, EventArgs e)
    {
        var radioButton = sender as RadioButton;

        if (radioButton == null)
            throw new ArgumentException("Sender is not of object type");

        int categoryAsInt = int.Parse(radioButton.Value.ToString()!);

        UserCategorChoice = (Categories)categoryAsInt;
    }

    public void UpdateAlertData(in int input)
    {
        _alertDataToSend.Category = input;
    }
}