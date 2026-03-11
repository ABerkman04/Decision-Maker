using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

[Table("decision_results")]
public class DecisionResult : BaseModel
{
    [PrimaryKey("id")]
    public Guid Id { get; set; }

    [Column("decision_id")]
    public Guid DecisionId { get; set; }

    [Column("option_name")]
    public string OptionName { get; set; }

    [Column("score")]
    public double Score { get; set; }

    [Column("rank")]
    public int Rank { get; set; }
}