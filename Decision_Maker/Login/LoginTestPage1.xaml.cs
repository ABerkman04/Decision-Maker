using Supabase.Gotrue;
using Supabase;
using System.Text.Json;
using System.Diagnostics;

namespace Decision_Maker;

public partial class LoginTestPage1 : ContentPage
{
    public LoginTestPage1()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        // Võtame praeguse sisselogitud kasutaja
        var user = SupabaseService.Client.Auth.CurrentUser;

        if (user != null)
        {
            Debug.WriteLine(user.Id);
            Debug.WriteLine(user.Email);
            Debug.WriteLine(user.ConfirmedAt?.ToString() ?? "null");
            //Debug.WriteLine(user.AppMetadata?["role"]?.ToString() ?? "null");


            IdLabel.Text = $"Id: {user.Id}";
            EmailLabel.Text = $"Email: {user.Email}";
            ConfirmedAtLabel.Text = $"Confirmed At: {user.ConfirmedAt?.ToString() ?? "null"}";
            //RoleLabel.Text = $"Role: {user.AppMetadata?["role"]?.ToString() ?? "null"}";

            // Kuva AppMetadata ja UserMetadata JSON kujul
            AppMetadataLabel.Text = $"AppMetadata: {JsonSerializer.Serialize(user.AppMetadata)}";
            UserMetadataLabel.Text = $"UserMetadata: {JsonSerializer.Serialize(user.UserMetadata)}";
        }
        else
        {
            IdLabel.Text = "No user logged in";
        }
    }
}