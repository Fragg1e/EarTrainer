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

            quiz = new Quiz();
            LoadCurrentQuestion();
        }

        private async void LoadCurrentQuestion()
        {
            if (quiz == null || quiz.Questions == null || currentQuestionIndex >= quiz.Questions.Length)
            {
                ShowFinalResult();
                return;
            }

            Question currentQuestion = quiz.Questions[currentQuestionIndex];

            if (currentQuestion == null)
            {
                ShowFinalResult();
                return;
            }

            QuestionNumberText.Text = "Question " + (currentQuestionIndex + 1) + " of " + quiz.Questions.Length;
            FeedbackText.Text = "";
            Score.Text = "Score: " + quiz.Score;

            AnswerButton1.Content = currentQuestion.Options[0];
            AnswerButton2.Content = currentQuestion.Options[1];
            AnswerButton3.Content = currentQuestion.Options[2];
            AnswerButton4.Content = currentQuestion.Options[3];

            await PlayCurrentQuestionSounds();
        }

        private async Task PlayCurrentQuestionSounds()
        {
            Question currentQuestion = quiz.Questions[currentQuestionIndex];

            if (currentQuestion == null)
            {
                return;
            }

            DisableAnswerButtons();
            Console.WriteLine("Question: " + currentQuestion.ToString());
            Sound.PlaySound(currentQuestion.Interval.FirstNote.Name);
            await Task.Delay(1300);
            Sound.PlaySound(currentQuestion.Interval.SecondNote.Name);

            EnableAnswerButtons();
        }

        private async void ReplayButton_Click(object sender, RoutedEventArgs e)
        {
            FeedbackText.Text = "Replaying...";
            await PlayCurrentQuestionSounds();
            FeedbackText.Text = "";
        }

        private async void AnswerButton_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;

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

            currentQuestionIndex++;
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

        private void ShowFinalResult()
        {
            QuestionNumberText.Text = "Quiz complete!";
            FeedbackText.Text = $"Final score: {quiz.Score} / {quiz.Questions.Length}";
            Score.Text = "Score: " + quiz.Score;

            AnswerButton1.Visibility = Visibility.Collapsed;
            AnswerButton2.Visibility = Visibility.Collapsed;
            AnswerButton3.Visibility = Visibility.Collapsed;
            AnswerButton4.Visibility = Visibility.Collapsed;
            ReplayButton.Visibility = Visibility.Collapsed;
            NamePromptText.Visibility = Visibility.Visible;
            PlayerNameTextBox.Visibility = Visibility.Visible;
            SaveScoreButton.Visibility = Visibility.Visible;
            PlayerNameTextBox.Focus();
        }

        private void SaveScoreButton_Click(object sender, RoutedEventArgs e)
        {
            if (scoreSaved)
            {
                return;
            }

            string playerName = PlayerNameTextBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(playerName))
            {
                FeedbackText.Text = "Enter a name first.";
                return;
            }

            using (EarTrainerContext db = new EarTrainerContext())
            {
                HighScore highScore = new HighScore();
                highScore.PlayerName = playerName;
                highScore.Score = quiz.Score;
                highScore.TotalQuestions = quiz.Questions.Length;
                highScore.Difficulty = Properties.Settings.Default.Difficulty;
                highScore.DatePlayed = DateTime.Now;

                db.HighScores.Add(highScore);
                db.SaveChanges();
            }

            scoreSaved = true;
            FeedbackText.Text = "Score saved for " + playerName;
            PlayerNameTextBox.IsEnabled = false;
            SaveScoreButton.IsEnabled = false;
        }

        private void Return_To_Menu_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
