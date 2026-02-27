namespace Decision_Maker;

public partial class AHP : ContentPage
{
    public int Alternatives { get; set; } = 3;
    public int Criteria { get; set; } = 3;

    public AHP()
    {
        InitializeComponent();
        BindingContext = this;
    }

    // Alternatives
    private void OnAlternativesPlus(object sender, EventArgs e)
    {
        Alternatives++;
        OnPropertyChanged(nameof(Alternatives));
    }

    private void OnAlternativesMinus(object sender, EventArgs e)
    {
        if (Alternatives > 1)
        {
            Alternatives--;
            OnPropertyChanged(nameof(Alternatives));
        }
    }

    // Criteria
    private void OnCriteriaPlus(object sender, EventArgs e)
    {
        Criteria++;
        OnPropertyChanged(nameof(Criteria));
    }

    private void OnCriteriaMinus(object sender, EventArgs e)
    {
        if (Criteria > 1)
        {
            Criteria--;
            OnPropertyChanged(nameof(Criteria));
        }
    }

    // Buttons
    private void OnBackClicked(object sender, EventArgs e)
    {
        System.Diagnostics.Debug.WriteLine("Back clicked");
    }

    private void OnContinueClicked(object sender, EventArgs e)
    {
        System.Diagnostics.Debug.WriteLine($"Continue clicked");
        System.Diagnostics.Debug.WriteLine($"Alternatives: {Alternatives}");
        System.Diagnostics.Debug.WriteLine($"Criteria: {Criteria}");
    }
}