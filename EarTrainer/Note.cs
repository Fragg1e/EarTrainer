using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EarTrainer
{

    public class Note 
    {
        
        public string Name { get; set; }
        public int Number { get; set; }

        private static Random rnd = new Random();

        public static Dictionary<int, string> notes = new Dictionary<int, string>
        {
            { 0,  "C"  },
            { 1,  "C#" },
            { 2,  "D"  },
            { 3,  "D#" },
            { 4,  "E"  },
            { 5,  "F"  },
            { 6,  "F#" },
            { 7,  "G"  },
            { 8,  "G#" },
            { 9,  "A"  },
            { 10, "A#" },
            { 11, "B"  }
        };


        public static Note GetRandomNote()
        {
            
            int num = rnd.Next(notes.Count);

            Note note = new Note()
            {
                Name = notes[num],
                Number = num
            };

            return note;
        }


    }

}