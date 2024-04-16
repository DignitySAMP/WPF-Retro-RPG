using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RetroRPG.components.menu
{
    public class Windows
    {
        public static void RedirectWindow(Window owner, Type windowType)
        {
            Window newWindow = (Window)Activator.CreateInstance(windowType);

            newWindow.Owner = owner;
            newWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;

            newWindow.Width = owner.ActualWidth;
            newWindow.Height = owner.ActualHeight;

            if (owner.WindowState == WindowState.Maximized)
            {
                newWindow.WindowState = WindowState.Maximized;
            }
            else
            {
                newWindow.WindowState = WindowState.Normal;
            }

            newWindow.Show();
            owner.Hide();
        }


        private static System.Media.SoundPlayer soundPlayer;
        public static bool IsMusicMuted = false;

        public static void PlayMenuMusic()
        {
            if (soundPlayer == null && !IsMusicMuted)
            {
                soundPlayer = new System.Media.SoundPlayer(Properties.Resources.menu);
                soundPlayer.Play();
            }
        }

        public static void StopMenuMusic()
        {
            if (soundPlayer != null)
            {
                soundPlayer.Stop();
                soundPlayer.Dispose();
                soundPlayer = null; 
            }
        }
    }
}
