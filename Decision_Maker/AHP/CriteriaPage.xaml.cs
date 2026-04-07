using Decision_Maker.Models;
using Decision_Maker.Resources.Localization;
using Decision_Maker.Services;
using Supabase.Gotrue;
using System.Diagnostics;
using System.Linq;

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
            await DisplayAlertAsync(AppResources.Limit_reached, AppResources.Maximum_5_criteria_allowed, AppResources.OK);
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
            await DisplayAlertAsync(AppResources.Error, AppResources.Add_at_least_2_criteria, AppResources.OK);
            return;
        }
        Debug.WriteLine(DecisionManager.CurrentDecision.Name);
        foreach (var c in DecisionManager.CurrentDecision.Criteria)
        {
            Debug.WriteLine(c.Name);
        }

        await Navigation.PushAsync(new WeightsPage());
    }

    void DeleteCriteriaClicked(object sender, TappedEventArgs e)
    {
        var image = sender as Image;
        var gesture = image?.GestureRecognizers.FirstOrDefault() as TapGestureRecognizer;
        var criteria = gesture?.CommandParameter as DecisionData.Criterion;
        //var button = sender as Button;
        //var criteria = button?.CommandParameter as DecisionData.Criterion;

        if (criteria == null)
            return;

        DecisionManager.CurrentDecision.Criteria.Remove(criteria);

        RefreshList();
    }
}