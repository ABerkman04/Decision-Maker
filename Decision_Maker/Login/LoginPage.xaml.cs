using Decision_Maker.AHP;
using Decision_Maker.Resources.Localization;
using Supabase.Storage;
using System.Diagnostics;

namespace Decision_Maker.Login;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
    }

    public class ErrorResponse
    {
        public int code { get; set; }
        public string error_code { get; set; } = "";
        public string msg { get; set; } = "";
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        string email = EmailEntry.Text?.Trim() ?? "";
        string password = PasswordEntry.Text?.Trim() ?? "";

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            await DisplayAlertAsync(
                AppResources.Error,
                AppResources.Please_enter_email_and_password,
                AppResources.OK);
            return;
        }

        try
        {
            var session = await SupabaseService.Client!
                .Auth.SignInWithPassword(email, password);

            if (session.User != null)
            {
                await DisplayAlertAsync(
                    AppResources.Success,
                    string.Format(AppResources.Welcome_user, session.User.Email),
                    AppResources.OK);
                await Navigation.PushAsync(new DecisionsPage());
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);

            string userFriendlyMessage = AppResources.Login_failed; // vaikimisi

            try
            {
                // proovime JSON-i parse'ida
                var errorObj = System.Text.Json.JsonSerializer.Deserialize<ErrorResponse>(ex.Message);
                if (errorObj != null && !string.IsNullOrEmpty(errorObj.msg))
                {
                    // Kui on olemas msg, kasutame seda
                    userFriendlyMessage = TranslateErrorCode(errorObj.error_code, errorObj.msg);
                }
            }
            catch
            {
                // kui parse ebaõnnestub, jääb userFriendlyMessage vaikimisi
            }

            await DisplayAlertAsync(
                AppResources.Error,
                userFriendlyMessage,
                AppResources.OK);
        }
    }
    private string TranslateErrorCode(string errorCode, string defaultMsg)
    {
        return errorCode switch
        {
            "invalid_credentials" => AppResources.Invalid_credentials, // Lisa .resx failis
            "user_not_found" => AppResources.User_not_found,
            "email_not_verified" => AppResources.Email_not_verified,
            _ => defaultMsg
        };
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