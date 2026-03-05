using Decision_Maker.AHP;
using Decision_Maker.Login;
using Decision_Maker.Resources.Localization;
using System.Globalization;

namespace Decision_Maker.Test;

public partial class TestEnvironment : ContentPage
{
	public TestEnvironment()
	{
		InitializeComponent();
	}

    void ChangeLanguage(string languageCode)
    {
        var culture = new CultureInfo(languageCode);

        Thread.CurrentThread.CurrentCulture = culture;
        Thread.CurrentThread.CurrentUICulture = culture;

        AppResources.Culture = culture;

        // Recreate only this page
        var currentPage = new TestEnvironment();
        Navigation.InsertPageBefore(currentPage, this);
        Navigation.PopAsync();
    }

    private async void OnStartClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SignUpPage());
    }
    private async void OnAHPClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new DecisionsPage());
    }

    private async void OnSupabaseClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SupabasePage());
    }
    private async void OnLoginTestPageClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new LoginTestPage1());
    }
    private async void OnLoginClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new LoginPage());
    }
    private async void OnSignUpPageClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SignUpPage());
    }
    private async void OnEstoniantClicked(object sender, EventArgs e)
    {
        ChangeLanguage("et");
    }

    private async void OnEnglishClicked(object sender, EventArgs e)
    {
        ChangeLanguage("en");
    }
}