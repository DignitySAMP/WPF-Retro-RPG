using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using RetroRPG.components.menu;
using RetroRPG.components;
using System.Windows.Media;
using static RetroRPG.components.menu.Windows;

// For reference: https://wiki.ultimacodex.com/wiki/Ultima_IV_internal_formats#Introduction

namespace RetroRPG
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public WindowState mainWindowState;

        public MainWindow()
        {
            InitializeComponent();
            Closed += OnWindowClosed;
        }
        private void OnWindowClosed(object sender, System.EventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            AudioManager.PlayMenuMusic();
        }

        private void BtnStartGame_Click(object sender, RoutedEventArgs e)
        {
            AudioManager.StopMenuMusic();
            WindowUtilities.RedirectWindow(this, typeof(Menu_Start));
        }

        private void BtnLoadGame_Click(object sender, RoutedEventArgs e)
        {
            WindowUtilities.RedirectWindow(this, typeof(Menu_Load));
        }

        private void BtnQuitGame_Click(object sender, RoutedEventArgs e)
        {
            AudioManager.StopMenuMusic();
            Close();
        }

        private void BtnMusic_Click(object sender, RoutedEventArgs e)
        {
            switch(AudioManager.IsMusicMuted)
            {
                case true:
                {
                    AudioManager.IsMusicMuted = false;
                    AudioManager.PlayMenuMusic();

                    Brush brush = (Brush)new BrushConverter().ConvertFromString("#AA0000");
                    BtnMusic.Background = brush;
                    BtnMusic.Content = "Mute";
                    break;

                }
                case false:
                {
                    AudioManager.IsMusicMuted = true;
                    Brush brush = (Brush)new BrushConverter().ConvertFromString("#00AAAA");
                    BtnMusic.Background = brush;
                    BtnMusic.Content = "Play";
                    AudioManager.StopMenuMusic();
                    break;
                }
            }
        }
    }
}
