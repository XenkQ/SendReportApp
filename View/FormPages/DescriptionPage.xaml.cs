using CommunityToolkit.Maui.Core.Platform;
using MauiApp1.ViewModel.Forms;
using Microsoft.Maui.Controls.PlatformConfiguration.AndroidSpecific;

namespace MauiApp1.View.FormPages;

public partial class DescriptionPage : ContentPage
{
    public DescriptionPage(FormDescriptionViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
#if ANDROID
        App.Current?.On<Microsoft.Maui.Controls.PlatformConfiguration.Android>()
                   .UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Resize);
#endif
    }

    protected override void OnNavigatedFrom(NavigatedFromEventArgs args)
    {
        base.OnNavigatedFrom(args);
        KeyboardExtensions.HideKeyboardAsync(DescriptionField, default);
#if ANDROID
        App.Current?.On<Microsoft.Maui.Controls.PlatformConfiguration.Android>()
           .UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Pan);
#endif
    }
}