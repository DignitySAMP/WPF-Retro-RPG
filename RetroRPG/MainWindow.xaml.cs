using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using RetroRPG.components.menu;
using RetroRPG.components;
using System.Windows.Media;

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
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Windows.PlayMenuMusic();
        }

        private void BtnStartGame_Click(object sender, RoutedEventArgs e)
        {
            Windows.StopMenuMusic();
            Windows.RedirectWindow(this, typeof(Menu_Start));
        }

        private void BtnLoadGame_Click(object sender, RoutedEventArgs e)
        {
            Windows.RedirectWindow(this, typeof(Menu_Load));
        }

        private void BtnQuitGame_Click(object sender, RoutedEventArgs e)
        {
            Windows.StopMenuMusic();
            Close();
        }

        private void BtnMusic_Click(object sender, RoutedEventArgs e)
        {
            switch(Windows.IsMusicMuted)
            {
                case true:
                {
                    Windows.IsMusicMuted = false;
                    Windows.PlayMenuMusic();

                    Brush brush = (Brush)new BrushConverter().ConvertFromString("#AA0000");
                    BtnMusic.Background = brush;
                    BtnMusic.Content = "Mute";
                    break;

                }
                case false:
                {
                    Windows.IsMusicMuted = true;
                    Brush brush = (Brush)new BrushConverter().ConvertFromString("#00AAAA");
                    BtnMusic.Background = brush;
                    BtnMusic.Content = "Play";
                    Windows.StopMenuMusic();
                    break;
                }
            }
        }
    }
}
