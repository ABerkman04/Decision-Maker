namespace Decision_Maker.NavBarResults;

using Decision_Maker.Components;
using System.Diagnostics;


public partial class NavResultsListPage : ContentPage
{
	public NavResultsListPage()
	{
		InitializeComponent();

        NavigationBar.SetActive("results");

        LoadDecisions();
    }

    async void LoadDecisions()
    {
        try
        {
            var user = SupabaseService.Client.Auth.CurrentUser;

            if (user == null)
                return;

            var response = await SupabaseService.Client
                .From<Decision>()
                .Filter("user_id", Supabase.Postgrest.Constants.Operator.Equals, user.Id)
                .Get();

            ResultsList.ItemsSource = response.Models;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
        }
    }
    async void DecisionClicked(object sender, EventArgs e)
    {
        var button = sender as Button;

        if (button?.CommandParameter is Guid decisionId)
        {
            Debug.WriteLine($"Decision clicked: {decisionId}");

            await Navigation.PushAsync(new NavResultsPage(decisionId));
        }
    }
}