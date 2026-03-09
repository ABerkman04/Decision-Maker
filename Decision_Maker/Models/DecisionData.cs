using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Decision_Maker.Models;

public class DecisionData
{
    public class Criterion : INotifyPropertyChanged
    {
        public string Name { get; set; }

        double rawWeight = 5;
        public double RawWeight
        {
            get => rawWeight;
            set
            {
                if (rawWeight != value)
                {
                    rawWeight = value;
                    OnPropertyChanged();
                }
            }
        }
        public double Weight { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

    public string Name { get; set; } = "";

    public List<Criterion> Criteria { get; set; } = new();

    public List<string> Options { get; set; } = new();

    public List<List<List<double>>> criteriaMatrix = new();

}