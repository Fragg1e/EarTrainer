using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace EarTrainer
{
    public partial class HighScoresPage : Page
    {
        private const string AllDifficultiesOption = "All Difficulties";
        private const string HighestScoreOption = "Highest Score";
        private const string MostRecentOption = "Most Recent";
        private const string PlayerNameOption = "Player Name";

        public HighScoresPage()
        {
            InitializeComponent();
            LoadHighScores();
        }

        private void LoadHighScores()
        {
            using (EarTrainerContext db = new EarTrainerContext())
            {
                var highScores = db.HighScores.AsQueryable();

                string playerSearch = PlayerSearchTextBox.Text.Trim();
                if (!string.IsNullOrWhiteSpace(playerSearch))
                {
                    highScores = highScores.Where(h => h.PlayerName.Contains(playerSearch));
                }

                string selectedDifficulty = GetSelectedComboBoxText(DifficultyFilterComboBox);
                if (!string.IsNullOrWhiteSpace(selectedDifficulty) && selectedDifficulty != AllDifficultiesOption)
                {
                    highScores = highScores.Where(h => h.Difficulty == selectedDifficulty);
                }

                string sortBy = GetSelectedComboBoxText(SortByComboBox);
                highScores = ApplySorting(highScores, sortBy);

                HighScoresDataGrid.ItemsSource = highScores
                    .Take(10)
                    .ToList();
            }
        }

        private IQueryable<HighScore> ApplySorting(IQueryable<HighScore> highScores, string sortBy)
        {
            switch (sortBy)
            {
                case MostRecentOption:
                    return highScores
                        .OrderByDescending(h => h.DatePlayed)
                        .ThenByDescending(h => h.Score);
                case PlayerNameOption:
                    return highScores
                        .OrderBy(h => h.PlayerName)
                        .ThenByDescending(h => h.Score);
                case HighestScoreOption:
                default:
                    return highScores
                        .OrderByDescending(h => h.Score)
                        .ThenBy(h => h.DatePlayed);
            }
        }

        private string GetSelectedComboBoxText(ComboBox comboBox)
        {
            ComboBoxItem selectedItem = comboBox.SelectedItem as ComboBoxItem;
            return selectedItem?.Content?.ToString() ?? string.Empty;
        }

        private void FilterControl_Changed(object sender, RoutedEventArgs e)
        {
            if (!IsLoaded)
            {
                return;
            }

            LoadHighScores();
        }

        private void ClearFiltersButton_Click(object sender, RoutedEventArgs e)
        {
            PlayerSearchTextBox.Text = string.Empty;
            DifficultyFilterComboBox.SelectedIndex = 0;
            SortByComboBox.SelectedIndex = 0;
            LoadHighScores();
        }

        private void Return_To_Menu_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
