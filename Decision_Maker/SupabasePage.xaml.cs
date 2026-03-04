using Supabase;
using Supabase.Gotrue;
using Supabase.Postgrest;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
using System.Diagnostics;

namespace Decision_Maker;

public partial class SupabasePage : ContentPage
{
    public SupabasePage()
    {
        InitializeComponent();
    }

    // 🔹 Loeb ja kuvab kõik users
    private async void OnTestClicked(object sender, EventArgs e)
    {
        await LoadUsers();
    }

    private async Task LoadUsers()
    {
        var response = await SupabaseService.Client!
            .From<User>()
            .Get();

        UsersList.ItemsSource = response.Models;
    }

    // 🔹 Delete nupp iga rea kõrval
    private async void OnAddUserClicked(object sender, EventArgs e)
    {
        Debug.WriteLine(sender);
        string newName = $"User {DateTime.Now:HHmmss}";
        Debug.WriteLine(newName);

        var newUser = new User { Name = newName };
        Debug.WriteLine(newUser);

        // INSERT uues kliendis
        await SupabaseService.Client!.From<User>().Insert(newUser);

        await LoadUsers();
    }

    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var userId = (int)button.CommandParameter;

        await SupabaseService.Client!
            .From<User>()
            .Where(x => x.Id == userId) // filter
            .Delete();                  // DELETE päring

        await LoadUsers();
    }
}

[Table("decisions")]
public class User : BaseModel
{
    [PrimaryKey("id")]
    public int Id { get; set; }

    [Column("choice_name")]
    public string Name { get; set; }
}