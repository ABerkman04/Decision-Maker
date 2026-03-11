using Decision_Maker.AHP;
using Decision_Maker.Login;
using Decision_Maker.NavBarResults;
using Decision_Maker.Services;

namespace Decision_Maker.Components;

public partial class NavigationBar : ContentView
{
    public NavigationBar()
    {
        InitializeComponent();

        bool isLoggedIn = SupabaseService.Client.Auth.CurrentUser != null;

        if (!isLoggedIn)
        {
            ResultsButton.Opacity = 0.4;
            //ResultsButton.IsEnabled = false;
        }
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
        if (SupabaseService.Client.Auth.CurrentUser == null)
        {
            bool login = await Application.Current.MainPage.DisplayAlertAsync(
                "Login required",
                "You need to log in to save results. Do you want to log in or continue as guest?",
                "Login",
                "Continue as Guest");

            if (login)
            {
                await Navigation.PushAsync(new LoginPage());
                return;
            }
        }
        else if (DecisionSession.IsDecisionInProgress)
        {
            bool answer = await Application.Current.MainPage.DisplayAlertAsync(
                "Leave decision?",
                "Your current decision will be lost.",
                "Leave",
                "Stay");

            if (!answer)
                return;

            DecisionSession.IsDecisionInProgress = false;

            await Navigation.PushAsync(new NavResultsListPage());
        }
        else
        {
            await Navigation.PushAsync(new NavResultsListPage());
        }
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