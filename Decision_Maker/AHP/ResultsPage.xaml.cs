using Decision_Maker.Services;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace Decision_Maker.AHP;

public partial class ResultsPage : ContentPage
{
    public class ResultItem
    {
        public string Option { get; set; }
        public double Score { get; set; }
    }

    public ResultsPage()
    {
        InitializeComponent();

        LoadResults();

        if (SupabaseService.Client.Auth.CurrentSession != null)
        {
            SaveDecisionToDatabase();
        }
    }

    void LoadResults()
    {
        var decision = DecisionManager.CurrentDecision;

        var results = new List<ResultItem>();

        for (int i = 0; i < decision.Options.Count; i++)
        {
            results.Add(new ResultItem
            {
                Option = decision.Options[i],
                Score = decision.finalScore[0][i]
            });
        }

        // Sort best → worst
        results = results.OrderByDescending(r => r.Score).ToList();

        ResultsList.ItemsSource = results;

        DecisionNameLabel.Text = DecisionManager.CurrentDecision.Name;
    }

    async void SaveDecisionToDatabase()
    {
        var decision = DecisionManager.CurrentDecision;

        var userId = SupabaseService.Client.Auth.CurrentUser.Id;

        // 1️⃣ Insert decision
        var decisionModel = new Decision
        {
            UserId = userId,
            Name = decision.Name
        };

        var decisionResponse = await SupabaseService.Client
            .From<Decision>()
            .Insert(new List<Decision> { decisionModel });

        var decisionId = decisionResponse.Models.First().Id;

        // 2️⃣ Insert criteria
        foreach (var c in decision.Criteria)
        {
            var criteria = new DecisionCriteria
            {
                DecisionId = decisionId,
                Name = c.Name,
                Weight = c.Weight
            };

            await SupabaseService.Client
                .From<DecisionCriteria>()
                .Insert(new List<DecisionCriteria> { criteria });
        }

        // 3️⃣ Insert options
        foreach (var option in decision.Options)
        {
            var optionModel = new DecisionOption
            {
                DecisionId = decisionId,
                Name = option
            };

            await SupabaseService.Client
                .From<DecisionOption>()
                .Insert(new List<DecisionOption> { optionModel });
        }

        // 4️⃣ Insert results
        var ranked = decision.Options
            .Select((o, i) => new { Option = o, Score = decision.finalScore[0][i] })
            .OrderByDescending(x => x.Score)
            .ToList();

        for (int i = 0; i < ranked.Count; i++)
        {
            var result = new DecisionResult
            {
                DecisionId = decisionId,
                OptionName = ranked[i].Option,
                Score = ranked[i].Score,
                Rank = i + 1
            };

            await SupabaseService.Client
                .From<DecisionResult>()
                .Insert(new List<DecisionResult> { result });
        }
    }
}