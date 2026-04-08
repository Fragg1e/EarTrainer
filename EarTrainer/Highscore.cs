using System;

namespace EarTrainer
{
    public class HighScore
    {
        public int HighScoreId { get; set; }

        public string PlayerName { get; set; }

        public int Score { get; set; }

        public int TotalQuestions { get; set; }

        public string Difficulty { get; set; }

        public DateTime DatePlayed { get; set; }
    }
}