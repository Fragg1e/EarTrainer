using System.Windows;
using System.Windows.Controls;

namespace EarTrainer
{
    public partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            InitializeComponent();
            LoadSettings();
        }

        private void LoadSettings()
        {
            if (Settings.Difficulty == "Easy")
            {
                EasyRadioButton.IsChecked = true;
            }
            else if (Settings.Difficulty == "Medium")
            {
                MediumRadioButton.IsChecked = true;
            }
            else if (Settings.Difficulty == "Hard")
            {
                HardRadioButton.IsChecked = true;
            }

            VolumeSlider.Value = Settings.Volume * 100;
            VolumeTextBlock.Text = ((int)VolumeSlider.Value).ToString() + "%";
        }

        private void Difficulty_Checked(object sender, RoutedEventArgs e)
        {
            if (EasyRadioButton.IsChecked == true)
            {
                Settings.Difficulty = "Easy";
            }
            else if (MediumRadioButton.IsChecked == true)
            {
                Settings.Difficulty = "Medium";
            }
            else if (HardRadioButton.IsChecked == true)
            {
                Settings.Difficulty = "Hard";
            }
        }

        private void VolumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (VolumeTextBlock != null)
            {
                VolumeTextBlock.Text = ((int)VolumeSlider.Value).ToString() + "%";
            }

            Settings.Volume = VolumeSlider.Value / 100.0;
        }

        private void Return_To_Menu_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}