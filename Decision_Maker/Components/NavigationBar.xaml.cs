using Decision_Maker.AHP;
using Decision_Maker.Services;

namespace Decision_Maker.Components;

public partial class NavigationBar : ContentView
{
    public NavigationBar()
    {
        InitializeComponent();
    }

    async void GoHome(object sender, EventArgs e)
    {
        if (DecisionSession.IsDecisionInProgress)
        {
            bool answer = await Application.Current.MainPage.DisplayAlertAsync(
                "Leave decision?",
                "Your current decision will be lost.",
                "Leave",
                "Stay");

            if (!answer)
                return;

            DecisionSession.IsDecisionInProgress = false;
        }

        await Navigation.PushAsync(new DecisionsPage());
    }

    async void GoResults(object sender, EventArgs e)
    {
        if (DecisionSession.IsDecisionInProgress)
        {
            bool answer = await Application.Current.MainPage.DisplayAlertAsync(
                "Leave decision?",
                "Your current decision will be lost.",
                "Leave",
                "Stay");

            if (!answer)
                return;

            DecisionSession.IsDecisionInProgress = false;
        }

        await Navigation.PushAsync(new DecisionsPage());
    }

    async void GoSettings(object sender, EventArgs e)
    {
        if (DecisionSession.IsDecisionInProgress)
        {
            bool answer = await Application.Current.MainPage.DisplayAlertAsync(
                "Leave decision?",
                "Your current decision will be lost.",
                "Leave",
                "Stay");

            if (!answer)
                return;

            DecisionSession.IsDecisionInProgress = false;
        }

        await Navigation.PushAsync(new DecisionsPage());
    }
}