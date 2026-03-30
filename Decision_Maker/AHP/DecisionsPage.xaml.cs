namespace Decision_Maker.AHP;

public partial class DecisionsPage : ContentPage
{
    public DecisionsPage()
    {
        InitializeComponent();

        NavigationBar.SetActive("home");
        if (SupabaseService.Client.Auth.CurrentUser != null)
        {
            var user = SupabaseService.Client.Auth.CurrentUser;

            var displayName =
                user.UserMetadata?["display_name"]?.ToString();
            DisplayLabel.Text = "Welcome " + displayName + "!";
        }
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