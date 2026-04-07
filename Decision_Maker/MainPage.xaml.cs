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


    void ChangeLanguage(string languageCode)
    {
        var culture = new CultureInfo(languageCode);

        Thread.CurrentThread.CurrentCulture = culture;
        Thread.CurrentThread.CurrentUICulture = culture;

        AppResources.Culture = culture;

        Preferences.Set("app_language", languageCode);

        var currentPage = new MainPage();
        Navigation.InsertPageBefore(currentPage, this);
        Navigation.PopAsync();
    }
    private void EnglishClicked(object sender, EventArgs e)
    {
        ChangeLanguage("en");
    }

    private void EstonianClicked(object sender, EventArgs e)
    {
        ChangeLanguage("et");
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();

        var lang = Preferences.Get("app_language", "en");
        SetActiveLanguage(lang);
    }
    void SetActiveLanguage(string lang)
    {
        EstonianLine.IsVisible = false;
        EnglishLine.IsVisible = false;

        if (lang == "et")
            EstonianLine.IsVisible = true;
        else if (lang == "en")
            EnglishLine.IsVisible = true;
    }

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