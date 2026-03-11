namespace Decision_Maker.NavBarResults;

using System.Diagnostics;


public partial class NavResultsPage : ContentPage
{
    Guid decisionId;

    public NavResultsPage(Guid id)
    {
        InitializeComponent();

        decisionId = id;

        LoadResults();
    }

    async void LoadResults()
    {
        try
        {
            var resultsResponse = await SupabaseService.Client
                .From<DecisionResult>()
                .Filter("decision_id", Supabase.Postgrest.Constants.Operator.Equals, decisionId.ToString())
                .Order("rank", Supabase.Postgrest.Constants.Ordering.Ascending)
                .Get();

            ResultsList.ItemsSource = resultsResponse.Models;

            var decisionResponse = await SupabaseService.Client
                .From<Decision>()
                .Filter("id", Supabase.Postgrest.Constants.Operator.Equals, decisionId.ToString())
                .Single();

            DecisionNameLabel.Text = decisionResponse.Name;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
        }
    }
}