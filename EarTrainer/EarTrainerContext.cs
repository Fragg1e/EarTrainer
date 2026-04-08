using System.Collections.Generic;
using System.Data.Entity;
using System.Runtime.Remoting.Contexts;

namespace EarTrainer
{
    public class EarTrainerContext : DbContext
    {
        public EarTrainerContext() : base("name=EarTrainerContext")
        {
        }

        public DbSet<HighScore> HighScores { get; set; }
    }
}