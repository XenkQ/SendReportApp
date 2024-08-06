
namespace MauiApp1;

public partial class MainPage : ContentPage
{
    int count = 0;

    public MainPage()
    {
        InitializeComponent();
    }

    private void OnSendButtonClick(object sender, EventArgs e)
    {
        Console.WriteLine("Sending");
    }

    private void OnNextButtonClick(object sender, EventArgs e)
    {
        Console.WriteLine("Next");
    }

    private async void OnTakePhotoClick(object sender, EventArgs e) 
    {
        await TakePhoto();
    }

    private async Task TakePhoto()
    {
        try
        {
            var photo = await MediaPicker.CapturePhotoAsync();

            if(photo != null)
            {
                var stream = await photo.OpenReadAsync();
                //photoResultImage.Source = ImageSource.FromStream(() => stream);
                Console.WriteLine(photo.FileName);
            }
        }
        catch(Exception ex)
        {
            await DisplayAlert("Error", $"Can't take photo: {ex.Message}", "OK");
        }
    }
}
