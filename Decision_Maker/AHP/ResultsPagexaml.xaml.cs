using Decision_Maker.Services;

namespace Decision_Maker.AHP;

public partial class ResultsPagexaml : ContentPage
{
    public class ResultItem
    {
        public string Option { get; set; }
        public double Score { get; set; }
    }

    public ResultsPagexaml()
    {
        InitializeComponent();

        LoadResults();
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
    }
}