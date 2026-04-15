using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SQLite;
using System.IO;
using System.Windows;

namespace EarTrainer
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Database.SetInitializer(new CreateDatabaseIfNotExists<EarTrainerContext>());

            using (var db = new EarTrainerContext())
            {
                db.Database.Initialize(false);
            }

            MigrateHighScoresSchema();
        }

        private void MigrateHighScoresSchema()
        {
            string databasePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "EarTrainer.db");

            if (!File.Exists(databasePath))
            {
                return;
            }

            using (var connection = new SQLiteConnection($"Data Source={databasePath};Version=3;"))
            {
                connection.Open();

                if (!TableExists(connection, "HighScores"))
                {
                    return;
                }

                List<string> columns = GetTableColumns(connection, "HighScores");
                if (!columns.Contains("TotalQuestions"))
                {
                    return;
                }

                string migrationSql = @"
BEGIN TRANSACTION;

CREATE TABLE HighScores_New (
    HighScoreId INTEGER PRIMARY KEY AUTOINCREMENT,
    PlayerName TEXT NOT NULL,
    Score INTEGER NOT NULL,
    Difficulty TEXT NOT NULL,
    DatePlayed DATETIME NOT NULL
);

INSERT INTO HighScores_New (HighScoreId, PlayerName, Score, Difficulty, DatePlayed)
SELECT HighScoreId, PlayerName, Score, Difficulty, DatePlayed
FROM HighScores;

DROP TABLE HighScores;
ALTER TABLE HighScores_New RENAME TO HighScores;

COMMIT;";

                using (var command = new SQLiteCommand(migrationSql, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        private bool TableExists(SQLiteConnection connection, string tableName)
        {
            using (var command = new SQLiteCommand("SELECT name FROM sqlite_master WHERE type = 'table' AND name = @tableName;", connection))
            {
                command.Parameters.AddWithValue("@tableName", tableName);
                return command.ExecuteScalar() != null;
            }
        }

        private List<string> GetTableColumns(SQLiteConnection connection, string tableName)
        {
            List<string> columns = new List<string>();

            using (var command = new SQLiteCommand($"PRAGMA table_info({tableName});", connection))
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    columns.Add(reader["name"].ToString());
                }
            }

            return columns;
        }
    }
}
