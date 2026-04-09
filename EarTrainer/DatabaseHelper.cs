using System;
using System.Data.SQLite;
using System.IO;

namespace EarTrainer
{
    public static class DatabaseHelper
    {
        public static void Initialize()
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", AppDomain.CurrentDomain.BaseDirectory);

            string databasePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "EarTrainer.db");

            if (!File.Exists(databasePath))
            {
                SQLiteConnection.CreateFile(databasePath);
            }

            using (SQLiteConnection connection = new SQLiteConnection("Data Source=" + databasePath + ";Version=3;"))
            {
                connection.Open();

                string createTableSql = @"
                    CREATE TABLE IF NOT EXISTS HighScores (
                        HighScoreId INTEGER PRIMARY KEY AUTOINCREMENT,
                        PlayerName TEXT NOT NULL,
                        Score INTEGER NOT NULL,
                        TotalQuestions INTEGER NOT NULL,
                        Difficulty TEXT NOT NULL,
                        DatePlayed DATETIME NOT NULL
                    )";

                using (SQLiteCommand command = new SQLiteCommand(createTableSql, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
