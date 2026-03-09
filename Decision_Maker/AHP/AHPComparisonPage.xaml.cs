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

    int questionIndex = 1;

    List<List<double>> AlternativePriorityList = new ();

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
        questionIndex--;

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

        questionIndex ++;
        currentIndex++;

        // Kui kõik option comparisonid selle criteria jaoks tehtud
        if (currentIndex >= comparisons.Count)
        {
            currentIndex = 0;
            currentCriterionIndex++;

            // Kui kõik criteria tehtud
            if (currentCriterionIndex >= DecisionManager.CurrentDecision.Criteria.Count)
            {
                NormalizeMatrix();
                AlternativePriority();
                FinalScore();
                //PrintEachMatrix();
                await Navigation.PushAsync(new ResultsPagexaml());
                return;
            }
        }

        LoadQuestion();
    }

    void NormalizeMatrix()
    {
        var criteriaMatrixList = DecisionManager.CurrentDecision.criteriaMatrix;

        for (int c = 0; c < criteriaMatrixList.Count; c++)
        {
            var matrix = criteriaMatrixList[c];
            int n = matrix.Count;

            // 1. Sum each column
            double[] colSums = new double[n];
            for (int j = 0; j < n; j++)
            {
                double sum = 0;
                for (int i = 0; i < n; i++)
                    sum += matrix[i][j];
                colSums[j] = sum;
            }

            // 2. Divide each element by column sum and round to 3 decimals
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    matrix[i][j] = Math.Round(matrix[i][j] / colSums[j], 3);
                }
            }
        }
    }

    void AlternativePriority()
    {
        var criteriaMatrixList = DecisionManager.CurrentDecision.criteriaMatrix;
        int criteriaCount = criteriaMatrixList.Count;
        int optionsCount = DecisionManager.CurrentDecision.Options.Count;

        // Tühjenda või loo uus AlternativePriorityList
        AlternativePriorityList = new List<List<double>>();

        for (int c = 0; c < criteriaCount; c++)
        {
            var matrix = criteriaMatrixList[c];
            var priorities = new List<double>();

            for (int i = 0; i < optionsCount; i++)
            {
                double rowSum = 0;
                for (int j = 0; j < optionsCount; j++)
                {
                    rowSum += matrix[i][j];
                }
                // Keskmine rea elementidest → prioriteet
                priorities.Add(Math.Round(rowSum / optionsCount, 3));
            }

            AlternativePriorityList.Add(priorities);
        }

        // Debug print
        for (int c = 0; c < criteriaCount; c++)
        {
            Console.WriteLine($"Alternative priorities for {DecisionManager.CurrentDecision.Criteria[c].Name}:");
            for (int j = 0; j < optionsCount; j++)
            {
                Console.WriteLine($"Option {DecisionManager.CurrentDecision.Options[j]}: {AlternativePriorityList[c][j]}");
            }
            Console.WriteLine();
        }
    }

    void FinalScore()
    {
        var decision = DecisionManager.CurrentDecision;
        int criteriaCount = decision.Criteria.Count;
        int optionsCount = decision.Options.Count;

        // Loo/initialiseeri finalScore
        decision.finalScore = new List<List<double>>();
        // Iga criteria jaoks
        for (int c = 0; c < criteriaCount; c++)
        {
            var scores = new List<double>(new double[optionsCount]);
            decision.finalScore.Add(scores);
        }

        // Arvuta final score iga alternatiivi jaoks
        // Formula: FinalScore(option) = sum(criterionWeight * alternativePriority)
        var finalScores = new List<double>(new double[optionsCount]);

        for (int i = 0; i < optionsCount; i++)
        {
            double total = 0;
            for (int c = 0; c < criteriaCount; c++)
            {
                // Kasuta Criteria.Weight (või RawWeight normaliseeritud)
                double weight = decision.Criteria[c].Weight;
                double priority = AlternativePriorityList[c][i];

                total += weight * priority;
            }
            finalScores[i] = Math.Round(total, 3);
        }

        // Salvestame ühe rea finalScore listi
        decision.finalScore.Clear();
        decision.finalScore.Add(finalScores);

        // Debug print
        Console.WriteLine("Final Scores:");
        for (int i = 0; i < optionsCount; i++)
        {
            Console.WriteLine($"{decision.Options[i]}: {finalScores[i]}");
        }
    }

}