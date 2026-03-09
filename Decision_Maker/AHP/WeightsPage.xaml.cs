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
            var rounded = Math.Round(e.NewValue);

            if (slider.Value != rounded)
                slider.Value = rounded;

            c.RawWeight = rounded;
        }
    }
    void NormalizeWeights()
    {
        var criteria = DecisionManager.CurrentDecision.Criteria;

        double sum = criteria.Sum(c => c.RawWeight);

        foreach (var c in criteria)
        {
            c.Weight = c.RawWeight / sum;
        }
    }

    async void ContinueClicked(object sender, EventArgs e)
    {
        NormalizeWeights();

        Debug.WriteLine(DecisionManager.CurrentDecision.Name);

        foreach (var c in DecisionManager.CurrentDecision.Criteria)
        {
            Debug.WriteLine(c.Name);
            Debug.WriteLine(c.Weight);
            Debug.WriteLine(c.RawWeight);
        }

        await Navigation.PushAsync(new OptionsPage());
    }
}