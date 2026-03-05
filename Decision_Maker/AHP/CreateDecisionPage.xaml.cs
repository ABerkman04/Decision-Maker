using Decision_Maker.Services;
using Decision_Maker.Models;

namespace Decision_Maker.AHP;

public partial class CreateDecisionPage : ContentPage
{
    public CreateDecisionPage()
    {
        InitializeComponent();
    }

    async void ContinueClicked(object sender, EventArgs e)
    {
        var name = DecisionNameEntry.Text?.Trim();

        if (string.IsNullOrEmpty(name))
        {
            await DisplayAlertAsync("Error", "Please enter decision name", "OK");
            return;
        }

        DecisionManager.Reset();
        DecisionManager.CurrentDecision.Name = name;

        DecisionSession.IsDecisionInProgress = true;

        await Navigation.PushAsync(new CriteriaPage());
    }
}