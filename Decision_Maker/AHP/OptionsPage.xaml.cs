using Decision_Maker.Models;
using Decision_Maker.Resources.Localization;
using Decision_Maker.Services;
using System.Diagnostics;
using System.Linq;

namespace Decision_Maker.AHP;

public partial class OptionsPage : ContentPage
{
    public OptionsPage()
    {
        InitializeComponent();
        RefreshList();
    }

    void RefreshList()
    {
        OptionsList.ItemsSource = null;
        OptionsList.ItemsSource = DecisionManager.CurrentDecision.Options;
    }

    async void AddOptionClicked(object sender, EventArgs e)
    {
        var text = OptionEntry.Text?.Trim();

        if (string.IsNullOrEmpty(text))
            return;

        if (DecisionManager.CurrentDecision.Options.Count >= 5)
        {
            await DisplayAlertAsync(AppResources.Limit_reached, AppResources.Maximum_5_criteria_allowed, AppResources.OK);
            return;
        }

        DecisionManager.CurrentDecision.Options.Add(text);
        Debug.WriteLine(text);
        OptionEntry.Text = "";

        RefreshList();
    }

    async void ContinueClicked(object sender, EventArgs e)
    {
        if (DecisionManager.CurrentDecision.Options.Count < 2)
        {
            await DisplayAlertAsync(AppResources.Error, AppResources.Add_at_least_2_options, AppResources.OK);
            return;
        }

        Debug.WriteLine(DecisionManager.CurrentDecision.Name);
        foreach (var c in DecisionManager.CurrentDecision.Criteria)
        {
            Debug.WriteLine(c.Name);
            Debug.WriteLine(c.Weight);
        }
        foreach (var o in DecisionManager.CurrentDecision.Options)
        {
            Debug.WriteLine(o);
        }

        await Navigation.PushAsync(new AHPComparisonPage());
    }

    void DeleteOptionClicked(object sender, TappedEventArgs e)
    {
        var image = sender as Image;
        var gesture = image?.GestureRecognizers.FirstOrDefault() as TapGestureRecognizer;
        var option = gesture?.CommandParameter as string;

        if (option == null)
            return;

        DecisionManager.CurrentDecision.Options.Remove(option);

        RefreshList();
    }
}