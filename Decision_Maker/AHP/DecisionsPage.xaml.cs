namespace Decision_Maker.AHP;

public partial class DecisionsPage : ContentPage
{
    public DecisionsPage()
    {
        InitializeComponent();

        NavigationBar.SetActive("home");
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