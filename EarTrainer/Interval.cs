using System;

namespace EarTrainer
{
    public class Interval
    {
        public Tuple<Note, Note> Notes { get; set; }
        public string Name { get; set; }

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
            Notes = GetRandomInterval();
            Name = CalcInterval(Notes.Item1, Notes.Item2);
        }

        private string CalcInterval(Note from, Note to)
        {
            int distance = (to.Number - from.Number + 12) % 12;
            return GetIntervalName(distance);
        }

        private Tuple<Note, Note> GetRandomInterval()
        {
            Note note1 = new Note();
            Console.WriteLine($"Note 1: {note1.Name}");

            Note note2 = new Note();
            Console.WriteLine($"Note 2: {note2.Name}");

            while (note1 == note2)
            {
                note2 = new Note();
            }

            Tuple<Note, Note> interval = Tuple.Create(note1, note2);
            return interval;

        }
    }
}