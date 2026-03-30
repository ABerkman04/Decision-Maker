using Decision_Maker.Components;
using Decision_Maker.Login;
using Decision_Maker.Resources.Localization;
using Decision_Maker.Test;
using System.Diagnostics;
using System.Globalization;

namespace Decision_Maker.NawBarSettings;

public partial class NawBarSettingsPage : ContentPage
{
	public NawBarSettingsPage()
	{
		InitializeComponent();
        NavigationBar.SetActive("settings");

        if (SupabaseService.Client.Auth.CurrentUser != null)
        {
            var user = SupabaseService.Client.Auth.CurrentUser;

            EmailLabel.Text = user.Email;
        }
    }
    void ChangeLanguage(string languageCode)
    {
        var culture = new CultureInfo(languageCode);

        Thread.CurrentThread.CurrentCulture = culture;
        Thread.CurrentThread.CurrentUICulture = culture;

        AppResources.Culture = culture;

        // Recreate only this page
        var currentPage = new NawBarSettingsPage();
        Navigation.InsertPageBefore(currentPage, this);
        Navigation.PopAsync();
    }

    private void AboutClicked(object sender, EventArgs e)
    {
        DisplayAlertAsync("About", "About page", "OK");
    }

    private void LanguageClicked(object sender, EventArgs e)
    {
        LanguageOverlay.IsVisible = true;
    }
    private void CloseLanguageOverlay(object sender, EventArgs e)
    {
        LanguageOverlay.IsVisible = false;
    }

    private void EnglishClicked(object sender, EventArgs e)
    {
        ChangeLanguage("en");
        Debug.WriteLine("English");
        LanguageOverlay.IsVisible = false;
    }

    private void EstonianClicked(object sender, EventArgs e)
    {
        ChangeLanguage("et");
        Debug.WriteLine("Eesti keel");
        LanguageOverlay.IsVisible = false;
    }

    private async void LogoutClicked(object sender, EventArgs e)
    {
        bool confirm = await DisplayAlertAsync(
            "Logout",
            "Are you sure you want to logout?",
            "Logout",
            "Cancel");

        if (!confirm)
            return;

        try
        {
            await SupabaseService.Client.Auth.SignOut();

            await DisplayAlertAsync("Logout", "User logged out", "OK");

            Application.Current.MainPage = new NavigationPage(new MainPage());
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
        }
    }
}