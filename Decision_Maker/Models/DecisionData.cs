namespace Decision_Maker.Models;

public class DecisionData
{
    public class Criterion
    {
        public string Name { get; set; }

        public double Weight { get; set; } = 5;
    }


    public string Name { get; set; } = "";

    public List<Criterion> Criteria { get; set; } = new();

    public List<string> Options { get; set; } = new();

}