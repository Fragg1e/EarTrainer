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
            int numberOfQuestions = 0;
            CurrentQuestionIndex = 0;
            Score = 0;

            if (Difficulty == "Easy") //sets number of questions based on difficulty level
            {
                numberOfQuestions = 5;
            }
            else if(Difficulty == "Medium")
                {
                numberOfQuestions = 10;
            }
            else
            {
                numberOfQuestions = 15;
            }
            Questions = new Question[numberOfQuestions]; //creates a list with that many questions

            for (int i = 0; i < Questions.Length; i++)
            {
                Questions[i] = new Question(); //actually creates the questions
            }
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
