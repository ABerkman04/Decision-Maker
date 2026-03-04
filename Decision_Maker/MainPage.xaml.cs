using Decision_Maker.Login;
using Decision_Maker.Test;

namespace Decision_Maker;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new LoginPage());
    }
    private async void OnSignUpPageClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SignUpPage());
    }

    private async void OnTestEnvironmentClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new TestEnvironment());
    }
}