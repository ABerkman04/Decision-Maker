using Decision_Maker.AHP;
using Decision_Maker.Login;
using Decision_Maker.NavBarResults;
using Decision_Maker.NawBarSettings;
using Decision_Maker.Resources.Localization;
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
                AppResources.LeaveDecisionTitle,
                AppResources.LeaveDecisionMessage,
                AppResources.LeaveButton,
                AppResources.StayButton);

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
                AppResources.LoginRequiredTitle,
                AppResources.LoginRequiredMessage,
                AppResources.Log_in,
                AppResources.ContinueAsGuestButton);

            if (login)
            {
                await Navigation.PushAsync(new LoginPage());
                return;
            }
        }
        else if (DecisionSession.IsDecisionInProgress)
        {
            bool answer = await Application.Current.MainPage.DisplayAlertAsync(
                AppResources.LeaveDecisionTitle,
                AppResources.LeaveDecisionMessage,
                AppResources.LeaveButton,
                AppResources.StayButton);

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
        if (SupabaseService.Client.Auth.CurrentUser == null)
        {
            bool login = await Application.Current.MainPage.DisplayAlertAsync(
                AppResources.LoginRequiredTitle,
                AppResources.LoginRequiredMessage,
                AppResources.Log_in,
                AppResources.ContinueAsGuestButton);

            if (login)
            {
                await Navigation.PushAsync(new LoginPage());
                return;
            }
        }
        else if (DecisionSession.IsDecisionInProgress)
        {
            bool answer = await Application.Current.MainPage.DisplayAlertAsync(
                AppResources.LeaveDecisionTitle,
                AppResources.LeaveDecisionMessage,
                AppResources.LeaveButton,
                AppResources.StayButton);

            if (!answer)
                return;

            DecisionSession.IsDecisionInProgress = false;

            await Navigation.PushAsync(new NawBarSettingsPage());
        }
        else
        {
            await Navigation.PushAsync(new NawBarSettingsPage());
        }
    }
    public void SetActive(string page)
    {
        HomeLine.IsVisible = false;
        ResultsLine.IsVisible = false;
        SettingsLine.IsVisible = false;

        if (page == "home")
            HomeLine.IsVisible = true;

        if (page == "results")
            ResultsLine.IsVisible = true;

        if (page == "settings")
            SettingsLine.IsVisible = true;
    }
}