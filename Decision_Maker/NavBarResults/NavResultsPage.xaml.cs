namespace Decision_Maker.NavBarResults;

using Decision_Maker.AHP;
using Decision_Maker.Components;
using System.Diagnostics;


public partial class NavResultsPage : ContentPage
{
    Guid decisionId;

    public NavResultsPage(Guid id)
    {
        InitializeComponent();

        NavigationBar.SetActive("results");

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

            var results = resultsResponse.Models.ToList();

            // 1️⃣ arvutame kogusumma
            double totalScore = results.Sum(r => r.Score);

            // 2️⃣ normaliseerime nii, et summa oleks 100%
            var resultsWithPercent = results.Select(r =>
            {
                double percent = (r.Score / totalScore) * 100;
                return new
                {
                    r.OptionName,
                    Score = $"{percent:F1} %"  // ühe kümnendkoha täpsus
                };
            }).ToList();

            ResultsList.ItemsSource = resultsWithPercent;

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
    async void BackClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new NavResultsListPage());
    }
}