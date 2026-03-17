namespace Decision_Maker.Components;

public partial class PrimaryButton : ContentView
{
    public PrimaryButton()
    {
        InitializeComponent();
    }

    public static readonly BindableProperty ButtonTextProperty =
        BindableProperty.Create(
            nameof(ButtonText),
            typeof(string),
            typeof(PrimaryButton),
            string.Empty);

    public string ButtonText
    {
        get => (string)GetValue(ButtonTextProperty);
        set => SetValue(ButtonTextProperty, value);
    }

    public event EventHandler Clicked;

    private void InnerButton_Clicked(object sender, EventArgs e)
    {
        Clicked?.Invoke(this, e);
    }
}