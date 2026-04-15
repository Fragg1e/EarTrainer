using System.Data.Entity;
namespace EarTrainer
{
    public class EarTrainerContext : DbContext
    {
        public EarTrainerContext() : base("name=EarTrainerContext") { }

        public DbSet<HighScore> HighScores { get; set; }
    }
}