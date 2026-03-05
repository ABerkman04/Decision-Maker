using Decision_Maker.Models;
using Decision_Maker.Services;
using Supabase.Gotrue;
using System.Diagnostics;

namespace Decision_Maker.AHP;

public partial class CriteriaPage : ContentPage
{
    public CriteriaPage()
    {
        InitializeComponent();
        RefreshList();
    }

    void RefreshList()
    {
        CriteriaList.ItemsSource = null;
        CriteriaList.ItemsSource = DecisionManager.CurrentDecision.Criteria;
    }

    async void AddCriteriaClicked(object sender, EventArgs e)
    {
        var text = CriteriaEntry.Text?.Trim();

        if (string.IsNullOrEmpty(text))
            return;

        if (DecisionManager.CurrentDecision.Criteria.Count >= 5)
        {
            await DisplayAlertAsync("Limit reached", "Maximum 5 criteria allowed.", "OK");
            return;
        }

        DecisionManager.CurrentDecision.Criteria.Add(
        new DecisionData.Criterion
        {
            Name = text
        });

        CriteriaEntry.Text = "";

        RefreshList();
    }

    async void ContinueClicked(object sender, EventArgs e)
    {
        if (DecisionManager.CurrentDecision.Criteria.Count < 2)
        {
            await DisplayAlertAsync("Error", "Add at least 2 criteria.", "OK");
            return;
        }
        Debug.WriteLine(DecisionManager.CurrentDecision.Name);
        foreach (var c in DecisionManager.CurrentDecision.Criteria)
        {
            Debug.WriteLine(c.Name);
        }

        await Navigation.PushAsync(new WeightsPage());
    }

    void DeleteCriteriaClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var criteria = button?.CommandParameter as DecisionData.Criterion;

        if (criteria == null)
            return;

        DecisionManager.CurrentDecision.Criteria.Remove(criteria);

        RefreshList();
    }
}