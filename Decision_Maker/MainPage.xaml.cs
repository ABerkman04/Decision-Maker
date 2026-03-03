namespace Decision_Maker;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private async void OnStartClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new NewPage());
    }
    private async void OnAHPClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AHP());
    }

    private async void OnSupabaseClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SupabasePage());
    }
}