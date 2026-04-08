using System;

namespace EarTrainer
{
    public class Interval
    {
        public Note FirstNote { get; set; }
        public Note SecondNote { get; set; }
        public string Name { get; set; }

        private static Random random;

        public string GetIntervalName(int distance)
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

        public Interval()
        {
            FirstNote = new Note();
            SecondNote = new Note();

            while (FirstNote == SecondNote)
            {
                SecondNote = new Note();
            }

            Name = CalcInterval(FirstNote, SecondNote);
        }

        public Interval(Note firstNote, Note secondNote)
        {
            FirstNote = firstNote;
            SecondNote = secondNote;
            Name = CalcInterval(FirstNote, SecondNote);
        }

        private string CalcInterval(Note first, Note second)
        {
            int distance = (first.Number - second.Number + 12) % 12;
            return GetIntervalName(distance);
        }
    }
}