using System;

namespace EarTrainer
{
    public class HighScore
    {
        public int HighScoreId { get; set; }
        public string PlayerName { get; set; }
        public int Score { get; set; }
        public string Difficulty { get; set; }
        public DateTime DatePlayed { get; set; }

        public string DatePlayedDisplay
        {
            get { return DatePlayed.ToString("dd/MM/yyyy HH:mm"); }
        }
    }
}