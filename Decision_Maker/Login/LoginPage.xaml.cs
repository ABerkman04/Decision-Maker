using System.Diagnostics;

namespace Decision_Maker.Login;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        string email = EmailEntry.Text?.Trim() ?? "";
        string password = PasswordEntry.Text?.Trim() ?? "";

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            await DisplayAlertAsync("Error", "Please enter email and password.", "OK");
            return;
        }

        try
        {
            var session = await SupabaseService.Client!
                .Auth.SignInWithPassword(email, password);

            if (session.User != null)
            {
                await DisplayAlertAsync("Success", $"Welcome {session.User.Email}", "OK");
                await Navigation.PushAsync(new LoginTestPage1());
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            await DisplayAlertAsync("Login failed", ex.Message, "OK");
        }
    }

    private async void OnCreateAccountClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Login.SignUpPage());
    }

    private void OnForgotPasswordTapped(object sender, EventArgs e)
    {
        Debug.WriteLine("Forgot password clicked");
    }
}