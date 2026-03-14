using Decision_Maker.Components;
using Decision_Maker.Login;
using System.Diagnostics;

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

    private void AboutClicked(object sender, EventArgs e)
    {
        DisplayAlertAsync("About", "About page", "OK");
    }

    private void LanguageClicked(object sender, EventArgs e)
    {
        DisplayAlertAsync("Language", "Language settings", "OK");
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