using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace EarTrainer
{
    public partial class QuizPage : Page
    {
        private Quiz quiz;
        private int currentQuestionIndex = 0;
        private bool scoreSaved = false;

        public QuizPage()
        {
            InitializeComponent();

            quiz = new Quiz(); //create new quiz instance
            LoadCurrentQuestion();
            DifficultyText.Text = Properties.Settings.Default.Difficulty;
        }

        private async void LoadCurrentQuestion() //loads current question into the UI
        {
            if (quiz == null || quiz.Questions == null || currentQuestionIndex >= quiz.Questions.Length) //checks if quiz is finished
            {
                ShowFinalResult();
                return;
            }

            Question currentQuestion = quiz.Questions[currentQuestionIndex];

            QuestionNumberText.Text = "Question " + (currentQuestionIndex + 1) + " of " + quiz.Questions.Length;
            FeedbackText.Text = "";
            Score.Text = "Score: " + GetScorePercentage() + "%";

            AnswerButton1.Content = currentQuestion.Options[0];
            AnswerButton2.Content = currentQuestion.Options[1];
            AnswerButton3.Content = currentQuestion.Options[2];
            AnswerButton4.Content = currentQuestion.Options[3];

            await PlaySounds();
        }

        private async Task PlaySounds() //plays sounds for current question
        {
            Question currentQuestion = quiz.Questions[currentQuestionIndex];

            DisableAnswerButtons();

            Sound.PlaySound(currentQuestion.Interval.FirstNote.Name);
            await Task.Delay(1300); //using await to make sure the second note plays after first one is done
            Sound.PlaySound(currentQuestion.Interval.SecondNote.Name);

            EnableAnswerButtons();
        }

        private async void ReplayButton_Click(object sender, RoutedEventArgs e)
        {
            FeedbackText.Text = "Replaying...";
            await PlaySounds();
            FeedbackText.Text = "";
        }

        private async void AnswerButton_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button; //treats user click as a button object

            if (clickedButton == null)
            {
                return;
            }

            string selectedAnswer = clickedButton.Content.ToString();
            Question currentQuestion = quiz.Questions[currentQuestionIndex];

            if (selectedAnswer == currentQuestion.CorrectAnswer)
            {
                quiz.Score++;
                Sound.PlaySound("correct");
                FeedbackText.Text = "Correct!";
            }
            else
            {
                Sound.PlaySound("incorrect");
                FeedbackText.Text = "Wrong! Correct answer: " + currentQuestion.CorrectAnswer;
            }

            await Task.Delay(2000);

            currentQuestionIndex++; //moves on to next question
            LoadCurrentQuestion();
        }

        private void DisableAnswerButtons()
        {
            AnswerButton1.IsEnabled = false;
            AnswerButton2.IsEnabled = false;
            AnswerButton3.IsEnabled = false;
            AnswerButton4.IsEnabled = false;
        }

        private void EnableAnswerButtons()
        {
            AnswerButton1.IsEnabled = true;
            AnswerButton2.IsEnabled = true;
            AnswerButton3.IsEnabled = true;
            AnswerButton4.IsEnabled = true;
        }

        private void CollapseQuizButtons()
        {
            AnswerButton1.Visibility = Visibility.Collapsed;
            AnswerButton2.Visibility = Visibility.Collapsed;
            AnswerButton3.Visibility = Visibility.Collapsed;
            AnswerButton4.Visibility = Visibility.Collapsed;
            ReplayButton.Visibility = Visibility.Collapsed;
        }

        

        private void ShowFinalResult()
        {
            QuestionNumberText.Text = "Quiz complete!";
            FeedbackText.Text = $"Final score: {GetScorePercentage()}%";
            Score.Text = "Score: " + GetScorePercentage() + "%";

            CollapseQuizButtons();
            
            NamePromptText.Visibility = Visibility.Visible;
            PlayerNameTextBox.Visibility = Visibility.Visible;
            SaveScoreButton.Visibility = Visibility.Visible;

            PlayerNameTextBox.Focus(); //allows user to type straight away into name box
        }

        private void SaveScoreButton_Click(object sender, RoutedEventArgs e)
        {
            if (scoreSaved)
            {
                return;
            }

            string playerName = PlayerNameTextBox.Text.Trim(); //trims removes unwanted whitespace

            if (string.IsNullOrWhiteSpace(playerName)) //prevents entry into the database with missing name
            {
                FeedbackText.Text = "Enter a name first.";
                return;
            }

            using (EarTrainerContext db = new EarTrainerContext()) //creates a new entry into the database 
            {
                HighScore highScore = new HighScore
                {
                    PlayerName = playerName,
                    Score = GetScorePercentage(),
                    Difficulty = Properties.Settings.Default.Difficulty,
                    DatePlayed = DateTime.Now
                };

                db.HighScores.Add(highScore);
                db.SaveChanges();
            }

            NamePromptText.Visibility = Visibility.Collapsed;
            PlayerNameTextBox.Visibility = Visibility.Collapsed;
            SaveScoreButton.Visibility = Visibility.Collapsed;
            HighScoresBtn.Visibility = Visibility.Visible;

            scoreSaved = true;
            FeedbackText.Text = "Score saved for " + playerName;

        }

        private void Return_To_Menu_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainMenu());
        }

        private void HighScoresBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new HighScoresPage());
        }

        private int GetScorePercentage()
        {
            if (quiz == null || quiz.Questions == null || quiz.Questions.Length == 0)
            {
                return 0;
            }

            return (int)Math.Round((double)quiz.Score * 100 / quiz.Questions.Length);
        }
    }
}
