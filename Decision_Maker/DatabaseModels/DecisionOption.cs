using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

[Table("decision_options")]
public class DecisionOption : BaseModel
{
    [PrimaryKey("id")]
    public Guid Id { get; set; }

    [Column("decision_id")]
    public Guid DecisionId { get; set; }

    [Column("name")]
    public string Name { get; set; }
}