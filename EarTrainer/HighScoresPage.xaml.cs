using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace EarTrainer
{
    public partial class HighScoresPage : Page
    {
        public HighScoresPage()
        {
            InitializeComponent();
            LoadHighScores();
        }

        private void LoadHighScores()
        {
            using (EarTrainerContext db = new EarTrainerContext()) //gets database context and displays the top 10 high scores in the datagrid
            {
                var highScores = db.HighScores
                    .OrderByDescending(h => h.Score)
                    .ThenBy(h => h.DatePlayed)
                    .Take(10)
                    .ToList();

                HighScoresDataGrid.ItemsSource = highScores;
            }
        }

        private void Return_To_Menu_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainMenu());
        }
    }
}
