using CommunityToolkit.Maui.Core.Platform;
using MauiApp1.ViewModel.Forms;

#if ANDROID
using Microsoft.Maui.Controls.PlatformConfiguration.AndroidSpecific;
#endif

namespace MauiApp1.View.FormPages;

public partial class DescriptionPage : ContentPage
{
    public DescriptionPage(FormDescriptionViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;

        Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping("CursorColor", (handler, view) =>
        {
#if IOS
            handler.PlatformView.TintColor = UIKit.UIColor.Green;
#endif
        });
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