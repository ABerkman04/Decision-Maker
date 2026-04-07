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

    async void DeleteDecisionClicked(object sender, TappedEventArgs e)
    {
        var image = sender as Image;
        var gesture = image?.GestureRecognizers.FirstOrDefault() as TapGestureRecognizer;
        var decisionId = gesture?.CommandParameter as Guid?;

        if (decisionId == null)
            return;

        // Supabase Filter ei toeta Guid? – muuda stringiks
        var decisionIdStr = decisionId.ToString();

        // confirm (soovitatav!)
        bool confirm = await DisplayAlertAsync(
            "Delete",
            "Are you sure you want to delete this decision?",
            "Yes",
            "Cancel"
        );

        if (!confirm)
            return;

        try
        {
            // kustuta decision
            await SupabaseService.Client
                .From<Decision>()
                .Filter("id", Supabase.Postgrest.Constants.Operator.Equals, decisionIdStr)
                .Delete();

            //  refresh list
            LoadDecisions();
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);

            await DisplayAlertAsync("Error", "Failed to delete decision.", "OK");
        }
    }
}