using System;
using System.Collections.Generic;
using System.Linq;

namespace EarTrainer
{
    public class Question
    {
        public Interval Interval { get; set; }
        public string CorrectAnswer { get; set; }
        public List<string> Options { get; set; }

        public static Random random = new Random();

        private static readonly List<string> allOptions = new List<string>
        {
            "Unison",
            "Minor 2nd",
            "Major 2nd",
            "Minor 3rd",
            "Major 3rd",
            "Perfect 4th",
            "Tritone",
            "Perfect 5th",
            "Minor 6th",
            "Major 6th",
            "Minor 7th",
            "Major 7th"
        };

        public Question()
            : this(Properties.Settings.Default.Difficulty)
        {
        }

        public Question(string difficulty)
        {
            Interval = new Interval(difficulty);
            CorrectAnswer = Interval.Name;
            Options = GenerateOptions(difficulty);
        }

        public static void Shuffle(List<string> array)
        {
            for (int i = array.Count() - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);

                string temp = array[i];
                array[i] = array[j];
                array[j] = temp;
            }
        }

        private List<string> GenerateOptions(string difficulty)
        {
            List<string> selected = new List<string> { CorrectAnswer };
            List<string> availableOptions = GetAvailableOptions(difficulty);

            while (selected.Count < 4)
            {
                string option = availableOptions[random.Next(availableOptions.Count)];
                if (!selected.Contains(option))
                {
                    selected.Add(option);
                }
            }

            Shuffle(selected);
            return selected;
        }

        private List<string> GetAvailableOptions(string difficulty)
        {
            var allowedNames = Interval.GetAllowedDistances(difficulty)
                .Select(Interval.GetIntervalName)
                .Distinct()
                .ToList();

            return allOptions
                .Where(option => allowedNames.Contains(option))
                .ToList();
        }

        public override string ToString()
        {
            return $"Interval: {Interval.FirstNote.Name} to {Interval.SecondNote.Name} = {Interval.Name}";
        }
    }
}
