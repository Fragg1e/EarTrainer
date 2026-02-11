namespace EarTrainer
{
    public abstract class Question
    {
        
    }

    public class IntervalQuestion : Question
    {
        public Interval Interval { get; set; }

        public IntervalQuestion() : base()
        {
            Interval = Interval.GetRandomInterval();
        }

        public override string ToString()
        {
            return $"Interval: {Interval.Notes.Item1.Name} to {Interval.Notes.Item2.Name} = {Interval.Number}";
        }
    }

    public class ChordQuestion : Question
    {
        public Chord Chord { get; set; }
        public ChordQuestion() : base()
        {
            //Chord = Chord.GetRandomChord();
        }
    }
}
