namespace MauiApp1;

public partial class PhotoPage : ContentPage, IFlowButtonHolder
{
	public PhotoPage()
	{
		InitializeComponent();
	}

    public void OnBackButtonClick(object sender, EventArgs e)
    {
        ((App)Application.Current).LoadPage(Pages.MainPage);
    }

    public void OnNextButtonClick(object sender, EventArgs e)
    {
        ((App)Application.Current).LoadPage(Pages.DescriptionPage);
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

            if (photo != null)
            {
                var stream = await photo.OpenReadAsync();
                photoResultImage.Source = ImageSource.FromStream(() => stream);
                Console.WriteLine(photo.FileName);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Can't take photo: {ex.Message}", "OK");
        }
    }
}