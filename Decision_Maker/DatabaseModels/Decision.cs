using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

[Table("decisions")]
public class Decision : BaseModel
{
    [PrimaryKey("id")]
    public Guid Id { get; set; }

    [Column("user_id")]
    public string UserId { get; set; }

    [Column("name")]
    public string Name { get; set; }
}