using Supabase.Gotrue;
using System.Diagnostics;

namespace Decision_Maker;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        ErrorLabel.Text = "";
        string email = EmailEntry.Text?.Trim() ?? "";
        string password = PasswordEntry.Text?.Trim() ?? "";

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            ErrorLabel.Text = "Please enter email and password.";
            return;
        }

        try
        {
            var session = await SupabaseService.Client!.Auth.SignInWithPassword(email, password);

            if (session.User != null)
            {
                // Login edukas
                await DisplayAlertAsync("Success", $"Welcome {session.User.Email}", "OK");

                // Näiteks avame SupabasePage
                await Navigation.PushAsync(new LoginTestPage1());
            }
            else
            {
                ErrorLabel.Text = "Login failed. Check credentials.";
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            ErrorLabel.Text = "Login failed: " + ex.Message;
        }
    }

    private async void OnSignUpClicked(object sender, EventArgs e)
    {
        ErrorLabel.Text = "";
        string email = EmailEntry.Text?.Trim() ?? "";
        string password = PasswordEntry.Text?.Trim() ?? "";

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            ErrorLabel.Text = "Please enter email and password.";
            return;
        }

        try
        {
            var session = await SupabaseService.Client!.Auth.SignUp(email, password);

            if (session.User != null)
            {
                await DisplayAlertAsync("Success", $"User registered: {session.User.Email}", "OK");

                var loginSession = await SupabaseService.Client!.Auth.SignInWithPassword(email, password);

                if (loginSession.User != null)
                {
                    await Navigation.PushAsync(new LoginTestPage1());
                }
                else
                {
                    ErrorLabel.Text = "Could not log in after registration. Check your email.";
                }
            }
            else
            {
                ErrorLabel.Text = "Sign up failed.";
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            ErrorLabel.Text = "Sign up failed: " + ex.Message;
        }
    }
}