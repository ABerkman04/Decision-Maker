using Decision_Maker.Models;
using Decision_Maker.Services;
using System.Diagnostics;
using static Decision_Maker.Models.DecisionData;
using static System.Net.Mime.MediaTypeNames;

namespace Decision_Maker.AHP;

public partial class WeightsPage : ContentPage
{
    public WeightsPage()
    {
        InitializeComponent();

        WeightsList.ItemsSource = DecisionManager.CurrentDecision.Criteria;
    }

    void SliderChanged(object sender, ValueChangedEventArgs e)
    {
        var slider = sender as Slider;

        if (slider?.BindingContext is Criterion c)
        {
            c.Weight = Math.Round(e.NewValue);
        }
    }

    async void ContinueClicked(object sender, EventArgs e)
    {
        Debug.WriteLine(DecisionManager.CurrentDecision.Name);
        foreach (var c in DecisionManager.CurrentDecision.Criteria)
        {
            Debug.WriteLine(c.Name);
            Debug.WriteLine(c.Weight);
        }
        await Navigation.PushAsync(new OptionsPage());
    }
}