using System;
using System.Collections.Generic;
using System.Linq;

namespace EarTrainer
{
    public class Question
    {
        public Interval Interval { get; set; }
        public string CorrectAnswer {  get; set; }
        public List<string> Options {  get; set; }

        public static Random random = new Random();

        List<string> allOptions = new List<string>
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
            "Major 7th",
            "Octave"
        };

        public Question()
        {
            Interval = new Interval();
            CorrectAnswer = Interval.Name;
            Options = GenerateOptions();
        }

        public static void Shuffle(List<string> array)
        {
            

            for (int i = array.Count() - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);

                // swap
                string temp = array[i];
                array[i] = array[j];
                array[j] = temp;
            }
        }

        private List<string> GenerateOptions()
        {
            List<string> selected = new List<string>();
            selected.Add(CorrectAnswer);

            while (selected.Count < 4)
            {
                string option = allOptions[random.Next(allOptions.Count)];
                if (!selected.Contains(option))
                {
                    selected.Add(option);
                }
            }
            
            Shuffle(selected);
            return selected;

            
        }

        public override string ToString()
        {
            return $"Interval: {Interval.FirstNote.Name} to {Interval.SecondNote.Name} = {Interval.Name}";
        }




    }

   
        

       

    
    

    
}
