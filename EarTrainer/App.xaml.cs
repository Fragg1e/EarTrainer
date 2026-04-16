using System.Data.Entity;
using System.Data.SQLite;
using System.Windows;

namespace EarTrainer
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            using (var connection = new SQLiteConnection("Data Source=EarTrainer.db;Version=3;"))
            {
                connection.Open();

                using (var command = new SQLiteCommand(
                    @"CREATE TABLE IF NOT EXISTS HighScores (
                        HighScoreId INTEGER PRIMARY KEY AUTOINCREMENT,
                        PlayerName TEXT NOT NULL,
                        Score INTEGER NOT NULL,
                        Difficulty TEXT NOT NULL,
                        DatePlayed DATETIME NOT NULL
                    )", connection))
                {
                    command.ExecuteNonQuery();
                }
            }

            Database.SetInitializer(new CreateDatabaseIfNotExists<EarTrainerContext>());

            using (var db = new EarTrainerContext())
            {
                db.Database.Initialize(false);
            }
        }
    }
}
