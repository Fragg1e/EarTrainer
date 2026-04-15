using System.Windows;
using System.Windows.Controls;

namespace EarTrainer
{
    public partial class SettingsPage : Page
    {
        private bool isLoadingSettings;

        public SettingsPage()
        {
            InitializeComponent();
            LoadSettings();
        }

        private void LoadSettings()
        {
            isLoadingSettings = true;
            Properties.Settings.Default.Reload();

            if (Properties.Settings.Default.Difficulty == "Easy")
            {
                EasyRadioButton.IsChecked = true;
            }
            else if (Properties.Settings.Default.Difficulty == "Medium")
            {
                MediumRadioButton.IsChecked = true;
            }
            else if (Properties.Settings.Default.Difficulty == "Hard")
            {
                HardRadioButton.IsChecked = true;
            }

            VolumeSlider.Value = Properties.Settings.Default.Volume * 100;
            VolumeTextBlock.Text = ((int)VolumeSlider.Value).ToString() + "%";
            isLoadingSettings = false;
        }

        private void Difficulty_Checked(object sender, RoutedEventArgs e)
        {
            if (isLoadingSettings)
            {
                return;
            }

            if (EasyRadioButton.IsChecked == true)
            {
                Properties.Settings.Default.Difficulty = "Easy";
            }
            else if (MediumRadioButton.IsChecked == true)
            {
                Properties.Settings.Default.Difficulty = "Medium";
            }
            else if (HardRadioButton.IsChecked == true)
            {
                Properties.Settings.Default.Difficulty = "Hard";
            }

            Properties.Settings.Default.Save();
        }

        private void VolumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (isLoadingSettings)
            {
                return;
            }

            if (VolumeTextBlock != null)
            {
                VolumeTextBlock.Text = ((int)VolumeSlider.Value).ToString() + "%";
            }

            Properties.Settings.Default.Volume = VolumeSlider.Value / 100.0;
            Properties.Settings.Default.Save();
        }

        private void Return_To_Menu_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.Save();
            NavigationService.GoBack();
        }
    }
}
