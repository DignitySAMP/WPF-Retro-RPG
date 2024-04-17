using RetroRPG.components.menu;
using System;
using System.Threading.Tasks;
using System.Windows;
using static RetroRPG.components.menu.Windows;

namespace RetroRPG
{
    /// <summary>
    /// Interaction logic for Menu_Start.xaml
    /// </summary>
    public partial class Menu_Start : Window
    {
        public Menu_Start()
        {
            InitializeComponent();
            Closed += OnWindowClosed;
        }
        private void OnWindowClosed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        int typeSpeedInMS = 70;
        bool isNarrationActive = false;
        int textCurrentIndex = 0;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            isNarrationActive = true;
            LblDivider1.Visibility = Visibility.Visible;

            ShowStoryIntro();
        }

        string introText = "In the 13th-century realm, a squire emerges from the humble roots of a farmer's family, his journey a fusion of rustic toil and lofty aspirations.\n\n" + "Raised amidst the agrarian rhythm, his hands toughened by labor, he is drawn to knighthood after a chance encounter with a wandering knight. Guided by tales of valor, he seeks mentorship in the ways of chivalry, ascending from the fields to martial halls, mastering sword and shield.\n\n" + "Now, as he strides through the cobbled streets of the capital, his heart ablaze with determination, he embarks on the final leg of his odyssey—to be knighted and enshrined in the annals of noble valor.";
        string statsText = "You are in the village of Rumpertshire, where your family manages a pasture.\n\n" + "For your journey you have been given:\n" + "* 25 silver coins\n" + "* a loyal mule\n" + "* a dull shortsword\n" + "* and a leather body and chaps.\n\n" + "You should go speak to your father to start your journey.";

        private async void ShowStoryIntro()
        {
            PlayIntroNarration();
            textCurrentIndex = 0;
            while (textCurrentIndex < introText.Length)
            {
                if (isSectionSkipped)
                    break;

                else
                {
                    LblIntroText.Text += introText[textCurrentIndex];
                    textCurrentIndex++;
                    await Task.Delay(typeSpeedInMS);
                }
            }

            LblDivider2.Visibility = Visibility.Visible;
            LblDivider3.Visibility = Visibility.Visible;

            // Force set incase timings are behind.
            textCurrentIndex = introText.Length;
            LblIntroText.Text = introText;
            isSectionSkipped = true; // set this to true to resync with UpdateSkipButton()

            ShowStatsIntro();
        }
        private async void ShowStatsIntro()
        {
            while (textCurrentIndex < introText.Length)
            {
                await Task.Delay(100);
            }


            PlayStatsNarration();
            textCurrentIndex = 0;
            while (textCurrentIndex < statsText.Length)
            {
                if (!isSectionSkipped)
                    break;

                else
                {
                    LblStatsText.Text += statsText[textCurrentIndex];
                    textCurrentIndex++;
                    await Task.Delay(typeSpeedInMS);
                }
            }

            // Force set incase timings are behind.
            StopNarration();
            LblStatsText.Text = statsText;
            isNarrationActive = false;
            isSectionSkipped = false; // set this to true to resync with UpdateSkipButton()

            LblDivider4.Visibility = Visibility.Visible;
            UpdateSkipButton();
        }

        private void BtnSkip_Click(object sender, RoutedEventArgs e)
        {
            if(isNarrationActive)
            {
                UpdateSkipButton();
                BtnSkip.Content = "Continue";
            }

            else
            {
                WindowUtilities.RedirectWindow(this, typeof(GameView));
            }
        }

        bool isSectionSkipped = false;
        private void UpdateSkipButton()
        {
            switch(isSectionSkipped)
            {
                case false:
                {
                    isSectionSkipped = true;
                    BtnSkip.Content = "Start Game";
                    break;
                }
                case true:
                {
                    isSectionSkipped = false;
                    break;
                }
            }
        }

        public System.Media.SoundPlayer narrationSoundPlayer;

        public void PlayIntroNarration()
        {
            StopNarration();
            narrationSoundPlayer = new System.Media.SoundPlayer(Properties.Resources.intro_story);
            narrationSoundPlayer.Play();
            
        }
        public void PlayStatsNarration()
        {
            StopNarration();
            narrationSoundPlayer = new System.Media.SoundPlayer(Properties.Resources.intro_stats);
            narrationSoundPlayer.Play();
        }

        public void StopNarration()
        {
            if (narrationSoundPlayer != null)
                narrationSoundPlayer.Stop();
        }
    }
}
