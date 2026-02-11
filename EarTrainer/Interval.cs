using System;

namespace EarTrainer
{
    public class Interval
    {
        public Tuple<Note, Note> Notes { get; set; }
        public int Number { get; set; }

        public Interval(Tuple<Note, Note> notes)
        {
            Notes = notes;
            Number = GetIntervalNumber(notes.Item1, notes.Item2);
        }

        private static int GetIntervalNumber(Note from, Note to)
        {
            int a = from.Number;
            int b = to.Number;
            return (b - a + 12) % 12;
        }

        public static Interval GetRandomInterval()
        {
            Note note1 = Note.GetRandomNote();
            Console.WriteLine($"Note 1: {note1.Name}");

            Note note2 = Note.GetRandomNote();
            Console.WriteLine($"Note 2: {note2.Name}");

            while (note1 == note2)
            {
                note2 = Note.GetRandomNote();
            }

            int number = GetIntervalNumber(note1, note2);

            Tuple<Note, Note> interval = Tuple.Create(note1, note2);
            return new Interval(interval);

        }
    }
}