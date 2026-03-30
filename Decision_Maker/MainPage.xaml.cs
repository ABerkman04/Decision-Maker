using Decision_Maker.AHP;
using Decision_Maker.Login;
using Decision_Maker.NawBarSettings;
using Decision_Maker.Resources.Localization;
using Decision_Maker.Test;
using System.Globalization;
using Microsoft.Maui.Storage;

namespace Decision_Maker;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    //Keel
    string language = "en";
    void ChangeLanguage(string languageCode)
    {
        var culture = new CultureInfo(languageCode);

        Thread.CurrentThread.CurrentCulture = culture;
        Thread.CurrentThread.CurrentUICulture = culture;

        AppResources.Culture = culture;

        // Recreate only this page
        var currentPage = new MainPage();
        Navigation.InsertPageBefore(currentPage, this);
        Navigation.PopAsync();
    }
    private async void EnglishClicked(object sender, EventArgs e)
    {
        ChangeLanguage("en");
    }
    private async void EstonianClicked(object sender, EventArgs e)
    {
        ChangeLanguage("et");
    }
    //Keel lopp

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new LoginPage());
    }
    private async void OnSignUpPageClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SignUpPage());
    }

    private async void OnGuestClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new DecisionsPage());
    }
}