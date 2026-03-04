using Decision_Maker.Login;

namespace Decision_Maker;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private async void OnStartClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SignUpPage());
    }
    private async void OnAHPClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AHP());
    }

    private async void OnSupabaseClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SupabasePage());
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new LoginPage());
    }
    private async void OnLoginTestPageClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new LoginTestPage1());
    }
}