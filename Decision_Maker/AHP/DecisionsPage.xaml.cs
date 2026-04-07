namespace Decision_Maker.AHP;

using Decision_Maker.Resources.Localization;

public partial class DecisionsPage : ContentPage
{
    public DecisionsPage()
    {
        InitializeComponent();

        NavigationBar.SetActive("home");

        var user = SupabaseService.Client.Auth.CurrentUser;

        string displayName;

        if (user != null)
        {
            displayName = user.UserMetadata?["display_name"]?.ToString();

            if (string.IsNullOrEmpty(displayName))
            {
                displayName = "User"; // fallback kui nimi puudub
            }
        }
        else
        {
            displayName = AppResources.Guest; // kasutame lokaliseeritud sõna
        }

        DisplayLabel.Text = string.Format(AppResources.Welcome, displayName);
    }

    void CreateDecisionClicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new CreateDecisionPage());
    }

    async void GoHome(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new DecisionsPage());
    }

    async void GoResults(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new DecisionsPage());
    }

    async void GoSettings(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new DecisionsPage());
    }
}