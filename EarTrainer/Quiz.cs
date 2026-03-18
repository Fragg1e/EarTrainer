using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EarTrainer
{
    public class Quiz
    {
        public Question[] Questions { get; set; }


        public Quiz()
        {
            Questions = new IntervalQuestion[];
        }

        public void Round()
        {
            foreach (IntervalQuestion question in Questions)
            {
                var note1 = question.Interval.Notes.Item1.ToString();
                var note2 = question.Interval.Notes.Item2.ToString();

                Sound.PlaySound(note1);
                Sound.PlaySound(note2);

                Console.WriteLine(note1, note2);

                string guess = Console.ReadLine();

                if (Int32.Parse(guess) == question.Interval.Number) 
                {
                    Console.WriteLine("Correct!");
                }
                else
                {
                    Console.WriteLine("Incorrect. The correct answer was "  + question.Interval.Number);
                }                
            }
         
        }
    }

   
}
