using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace EarTrainer
{
    public partial class MainMenu : Page
    {
        public MainMenu()
        {
            InitializeComponent();
        }
        private void StartBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new QuizPage());
        }

        private void SettingsBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SettingsPage());
        }

        private void HighScoresBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new HighScoresPage());
        }
    }
}
