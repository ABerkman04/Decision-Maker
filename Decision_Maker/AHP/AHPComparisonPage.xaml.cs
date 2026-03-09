//using Android.Locations;
using Decision_Maker.Services;
using System.Diagnostics;

namespace Decision_Maker.AHP;

public partial class AHPComparisonPage : ContentPage
{

    List<(string A, string B)> comparisons = new();

    int currentIndex = 0;

    string selectedOption = "-";

    int strength = 5;

    int currentCriterionIndex = 0;

    int questionIndex = 0;

    //List<List<List<double>>> criteriaMatrix = new();

    public AHPComparisonPage()
    {
        InitializeComponent();

        GenerateComparisons();

        LoadQuestion();
    }

    void GenerateComparisons()
    {
        var options = DecisionManager.CurrentDecision.Options;

        for (int i = 0; i < options.Count; i++)
        {
            for (int j = i + 1; j < options.Count; j++)
            {
                comparisons.Add((options[i], options[j]));
            }
        }


        var criteria = DecisionManager.CurrentDecision.Criteria;


        for (int i = 0; i < criteria.Count; i++)
        {
            var matrix = new List<List<double>>();

            for (int j = 0; j < options.Count; j++)
            {
                var row = new List<double>();

                for (int k = 0; k < options.Count; k++)
                {
                    if (j == k)
                        row.Add(1);
                    else
                        row.Add(0);
                }

                matrix.Add(row);
            }

            DecisionManager.CurrentDecision.criteriaMatrix.Add(matrix);

        }
        PrintEachMatrix();
    }

    void PrintEachMatrix()
    {
        var criteria = DecisionManager.CurrentDecision.Criteria;
        var criteriaMatrix = DecisionManager.CurrentDecision.criteriaMatrix;

        for (int i = 0; i < criteriaMatrix.Count; i++)
        {
            Console.WriteLine("This is the matrix for: " + criteria[i].Name);
            for (int j = 0; j < criteriaMatrix[i].Count; j++)
            {
                for (int k = 0; k < criteriaMatrix[i][j].Count; k++)
                {
                    Console.Write(criteriaMatrix[i][j][k] + " ");
                }

                Console.WriteLine();
            }

            Console.WriteLine();
        }

        Console.WriteLine();
    }

    void LoadQuestion()
    {
        var pair = comparisons[currentIndex];
        var criterion = DecisionManager.CurrentDecision.Criteria[currentCriterionIndex].Name;
        questionIndex += 1;


        QuestionLabel.Text = $"Question {questionIndex}";

        CriterionLabel.Text = criterion;

        OptionALabel.Text = pair.A;
        OptionBLabel.Text = pair.B;

        OptionAButton.Text = pair.A;
        OptionBButton.Text = pair.B;

        selectedOption = "-";
        ChoiceLabel.Text = "Your Choice: -";
    }

    void OptionAClicked(object sender, EventArgs e)
    {
        selectedOption = comparisons[currentIndex].A;

        ChoiceLabel.Text = $"Your Choice: {selectedOption}";
    }

    void OptionBClicked(object sender, EventArgs e)
    {
        selectedOption = comparisons[currentIndex].B;

        ChoiceLabel.Text = $"Your Choice: {selectedOption}";
    }

    void SliderChanged(object sender, ValueChangedEventArgs e)
    {
        var slider = sender as Slider;

        int value = (int)Math.Round(e.NewValue);

        if (slider.Value != value)
            slider.Value = value;

        strength = value;

        UpdateStrengthLabel(value);
    }
    void UpdateStrengthLabel(int value)
    {
        string[] texts =
        {
        "",
        "Equally important",
        "Equally to moderately more important",
        "Moderately more important",
        "Moderately to strongly more important",
        "Strongly more important",
        "Strongly to very strongly more important",
        "Very strongly more important",
        "Very strongly to extremely more important",
        "Extremely more important"
    };

        StrengthLabel.Text = $"Strength: {value} - {texts[value]}";
    }

    void BackClicked(object sender, EventArgs e)
    {
        if (currentIndex == 0)
            return;

        currentIndex--;

        LoadQuestion();
    }

    async void ContinueClicked(object sender, EventArgs e)
    {
        if (selectedOption == "-")
        {
            await DisplayAlertAsync("Error", "Please select an option.", "OK");
            return;
        }

        var options = DecisionManager.CurrentDecision.Options;

        var pair = comparisons[currentIndex];

        int aIndex = options.IndexOf(pair.A);
        int bIndex = options.IndexOf(pair.B);

        double value = strength;

        if (selectedOption == pair.A)
        {
            DecisionManager.CurrentDecision.criteriaMatrix[currentCriterionIndex][aIndex][bIndex] = value;
            DecisionManager.CurrentDecision.criteriaMatrix[currentCriterionIndex][bIndex][aIndex] = 1.0 / value;
        }
        else
        {
            DecisionManager.CurrentDecision.criteriaMatrix[currentCriterionIndex][aIndex][bIndex] = 1.0 / value;
            DecisionManager.CurrentDecision.criteriaMatrix[currentCriterionIndex][bIndex][aIndex] = value;
        }
        PrintEachMatrix();

        currentIndex++;

        // Kui kõik option comparisonid selle criteria jaoks tehtud
        if (currentIndex >= comparisons.Count)
        {
            currentIndex = 0;
            currentCriterionIndex++;

            // Kui kõik criteria tehtud
            if (currentCriterionIndex >= DecisionManager.CurrentDecision.Criteria.Count)
            {
                await DisplayAlertAsync("Done", "All comparisons completed.", "OK");
                return;
            }
        }

        LoadQuestion();
    }

}