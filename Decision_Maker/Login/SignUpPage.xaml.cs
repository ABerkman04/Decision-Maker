using Decision_Maker.AHP;
using Decision_Maker.Resources.Localization;
using System.Diagnostics;
using System.Text.Json;

namespace Decision_Maker.Login;

public partial class SignUpPage : ContentPage
{
    public SignUpPage()
    {
        InitializeComponent();
    }
    public class ErrorResponse
    {
        public int code { get; set; }
        public string error_code { get; set; } = "";
        public string msg { get; set; } = "";
    }

    private string TranslateErrorCode(string errorCode, string defaultMsg)
    {
        return errorCode switch
        {
            "invalid_credentials" => AppResources.Invalid_credentials,
            "user_not_found" => AppResources.User_not_found,
            "email_not_verified" => AppResources.Email_not_verified,
            "invalid_email" => AppResources.Invalid_email,
            "validation_failed" => AppResources.Invalid_email_format,
            "email_already_registered" => AppResources.Email_already_registered,
            "weak_password" => AppResources.Weak_password,
            _ => defaultMsg
        };
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
            await DisplayAlertAsync(
                AppResources.Error,
                AppResources.All_fields_required,
                AppResources.OK);
            return;
        }

        if (password != repeatPassword)
        {
            await DisplayAlertAsync(
                AppResources.Error,
                AppResources.Passwords_do_not_match,
                AppResources.OK);
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
                await DisplayAlertAsync(
                    AppResources.Success,
                    AppResources.Account_created,
                    AppResources.OK);

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

            string userFriendlyMessage = AppResources.Sign_up_failed;

            try
            {
                var errorObj = JsonSerializer.Deserialize<ErrorResponse>(ex.Message);
                if (errorObj != null && !string.IsNullOrEmpty(errorObj.msg))
                {
                    userFriendlyMessage = TranslateErrorCode(errorObj.error_code, errorObj.msg);
                }
            }
            catch
            {
                // jääb vaikimisi
            }

            await DisplayAlertAsync(
                AppResources.Error,
                userFriendlyMessage,
                AppResources.OK);
        }
    }

    private async void OnSignInClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}