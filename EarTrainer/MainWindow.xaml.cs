using System.Windows;

namespace EarTrainer
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            MainFrame.Navigate(new MainMenu());    
        }       
    }
}
