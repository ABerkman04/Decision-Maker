using Decision_Maker.Services;
using System.Diagnostics;

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
            await DisplayAlertAsync("Limit reached", "Maximum 5 options allowed.", "OK");
            return;
        }

        DecisionManager.CurrentDecision.Options.Add(text);

        OptionEntry.Text = "";

        RefreshList();
    }

    async void ContinueClicked(object sender, EventArgs e)
    {
        if (DecisionManager.CurrentDecision.Options.Count < 2)
        {
            await DisplayAlertAsync("Error", "Add at least 2 options.", "OK");
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

        await DisplayAlertAsync("Next step", "Next page will be AHP comparison", "OK");
    }

    void DeleteOptionClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var option = button?.CommandParameter as string;

        if (option == null)
            return;

        DecisionManager.CurrentDecision.Options.Remove(option);

        RefreshList();
    }
}