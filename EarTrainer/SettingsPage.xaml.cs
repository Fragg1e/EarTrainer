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

            VolumeSlider.Value = Properties.Settings.Default.Volume;
            VolumeTextBlock.Text = ((int)VolumeSlider.Value).ToString() + "%";

            QuestionCountSlider.Value = Properties.Settings.Default.NumberOfQuestions;
            QuestionCountTextBlock.Text = Properties.Settings.Default.NumberOfQuestions.ToString();
        }

        private void Difficulty_Checked(object sender, RoutedEventArgs e)
        {
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
            if (VolumeTextBlock != null)
            {
                VolumeTextBlock.Text = ((int)VolumeSlider.Value).ToString() + "%";
            }

            Properties.Settings.Default.Volume = VolumeSlider.Value / 100.0;
            Properties.Settings.Default.Save();
        }

        private void QuestionCountSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (QuestionCountTextBlock != null)
            {
                QuestionCountTextBlock.Text = ((int)QuestionCountSlider.Value).ToString();
            }

            Properties.Settings.Default.NumberOfQuestions = (int)QuestionCountSlider.Value;
            Properties.Settings.Default.Save();
        }

        private void Return_To_Menu_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.Save();
            NavigationService.GoBack();
        }
    }
}