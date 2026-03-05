using Decision_Maker.Models;

namespace Decision_Maker.Services;

public static class DecisionManager
{
    public static DecisionData CurrentDecision { get; set; } = new();

    public static void Reset()
    {
        CurrentDecision = new DecisionData();
    }
}