using Decision_Maker.AHP;
using System.Diagnostics;

namespace Decision_Maker.Login;

public partial class SignUpPage : ContentPage
{
    public SignUpPage()
    {
        InitializeComponent();
    }

    private async void OnSignUpClicked(object sender, EventArgs e)
    {
        string displayName = DisplayNameEntry.Text?.Trim() ?? "";
        string email = EmailEntry.Text?.Trim() ?? "";
        string password = PasswordEntry.Text?.Trim() ?? "";
        string repeatPassword = RepeatPasswordEntry.Text?.Trim() ?? "";

        if (string.IsNullOrEmpty(displayName) ||
            string.IsNullOrEmpty(email) ||
            string.IsNullOrEmpty(password))
        {
            await DisplayAlertAsync("Error", "All fields are required.", "OK");
            return;
        }

        if (password != repeatPassword)
        {
            await DisplayAlertAsync("Error", "Passwords do not match.", "OK");
            return;
        }

        try
        {
            var options = new Supabase.Gotrue.SignUpOptions
            {
                Data = new Dictionary<string, object>
                {
                    { "display_name", displayName }
                }
            };

            var session = await SupabaseService.Client!
                .Auth.SignUp(email, password, options);

            if (session.User != null)
            {
                await DisplayAlertAsync("Success", "Account created!", "OK");

                // Auto login
                var loginSession = await SupabaseService.Client!
                    .Auth.SignInWithPassword(email, password);

                if (loginSession.User != null)
                {
                    await Navigation.PushAsync(new DecisionsPage());
                }
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            await DisplayAlertAsync("Sign up failed", ex.Message, "OK");
        }
    }

    private async void OnSignInClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}