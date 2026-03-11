using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

[Table("decision_criteria")]
public class DecisionCriteria : BaseModel
{
    [PrimaryKey("id")]
    public Guid Id { get; set; }

    [Column("decision_id")]
    public Guid DecisionId { get; set; }

    [Column("name")]
    public string Name { get; set; }

    [Column("weight")]
    public double Weight { get; set; }
}