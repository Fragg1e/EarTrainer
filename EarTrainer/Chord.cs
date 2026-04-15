using System.Collections.Generic;

namespace EarTrainer
{
    public class Chord
    {
        public string Name { get; set; }
        public List<int> Intervals { get; set; }
        public List<Note> Notes { get; set; }
        public Chord(string name, List<int> intervals, List<Note> notes)
        {
            Name = name;
            Intervals = intervals;
            Notes = notes;
        }
    }
}
