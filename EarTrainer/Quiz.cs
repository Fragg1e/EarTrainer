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
        public int CurrentQuestionIndex { get; set; }
        public int Score { get; set; }
        public string Difficulty { get; set; }

        public Quiz()
        {
            Difficulty = Properties.Settings.Default.Difficulty;
            Questions = new Question[Properties.Settings.Default.NumberOfQuestions];

            for (int i = 0; i < Questions.Length; i++)
            {
                Questions[i] = new Question(Difficulty);
            }

            CurrentQuestionIndex = 0;
            Score = 0;
        }

        public Question GetCurrentQuestion()
        {
            if (CurrentQuestionIndex < Questions.Length)
            {
                return Questions[CurrentQuestionIndex];
            }

            return null;
        }

        public void MoveToNextQuestion()
        {
            CurrentQuestionIndex++;
        }

        public bool IsFinished()
        {
            return CurrentQuestionIndex >= Questions.Length;
        }
    }
}
