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
        public string Difficulty { get; set; }

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
        {
            Difficulty = Properties.Settings.Default.Difficulty;
            Interval = new Interval();
            CorrectAnswer = Interval.Name;
            Options = GenerateOptions();
        }

        public static void Shuffle(List<string> array) //randomises answers so correct answer isn't always in the same place
        {
            for (int i = array.Count() - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);

                (array[j], array[i]) = (array[i], array[j]);
            }
        }

        private List<string> GenerateOptions() //gets the correct answer and 3 random incorrect answers based on the difficulty level, then shuffles them before returning the list
        {
            List<string> selected = new List<string> { CorrectAnswer };
            List<string> availableOptions = GetAvailableOptions();

            while (selected.Count < 4)
            {
                string option = availableOptions[random.Next(availableOptions.Count)];
                if (!selected.Contains(option)) //ensures no duplicate options
                {
                    selected.Add(option);
                }
            }

            Shuffle(selected);

            return selected;
        }

        private List<string> GetAvailableOptions() //filters options based on difficulty
        {
            var allowedNames = Interval.GetAllowedDistances(Difficulty)
                .Select(Interval.GetIntervalName)
                .Distinct()
                .ToList();

            return allOptions
                .Where(option => allowedNames.Contains(option))
                .ToList();
        }

        public override string ToString() //i used this for testing 
        {
            return $"Interval: {Interval.FirstNote.Name} to {Interval.SecondNote.Name} = {Interval.Name}";
        }
    }
}
