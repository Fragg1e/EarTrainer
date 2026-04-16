using System;
using System.Collections.Generic;

namespace EarTrainer
{
    public class Interval
    {
        public Note FirstNote { get; set; }
        public Note SecondNote { get; set; }
        public string Name { get; set; }

        private static readonly Random random = new Random();

        //defines distances for each difficulty level
        private static readonly List<int> EasyDistances = new List<int> { 0, 1, 2, 3, 4, 5, 7 };
        private static readonly List<int> MediumDistances = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        private static readonly List<int> HardDistances = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };

        public static string GetIntervalName(int distance)
        {
            switch (distance)
            {
                case 0: return "Unison";
                case 1: return "Minor 2nd";
                case 2: return "Major 2nd";
                case 3: return "Minor 3rd";
                case 4: return "Major 3rd";
                case 5: return "Perfect 4th";
                case 6: return "Tritone";
                case 7: return "Perfect 5th";
                case 8: return "Minor 6th";
                case 9: return "Major 6th";
                case 10: return "Minor 7th";
                case 11: return "Major 7th";
                case 12: return "Octave";
                default: return "Unknown";
            }
        }

        public Interval(): this(Properties.Settings.Default.Difficulty) //using this to create interval based on current difficulty setting without having to specify it every time
        {
        }

        public Interval(string difficulty)
        {
            FirstNote = new Note();
            int distance = GetRandomDistance(difficulty);
            int secondNoteNumber = (FirstNote.Number - distance + 12) % 12;
            SecondNote = new Note(secondNoteNumber);
            Name = GetIntervalName(distance);
        }

        public static List<int> GetAllowedDistances(string difficulty) //returns which list should be used based on difficulty level
        {
            switch (difficulty)
            {
                case "Easy":
                    return EasyDistances;
                case "Medium":
                    return MediumDistances;
                case "Hard":
                default:
                    return HardDistances;
            }
        }

        private int GetRandomDistance(string difficulty)
        {
            List<int> allowedDistances = GetAllowedDistances(difficulty);
            return allowedDistances[random.Next(allowedDistances.Count)];
        }
    }
}
