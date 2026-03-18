namespace EarTrainer
{
    public abstract class Question
    {
        public virtual Question[] GenQ(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                Questions[i] = new Question();
            }
            return Questions;
        }

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

        public override Question[] GenQ(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                Questions[i] = new IntervalQuestion();
            }
            return Questions;
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
