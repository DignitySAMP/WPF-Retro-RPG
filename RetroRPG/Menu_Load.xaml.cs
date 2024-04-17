using System;
using System.Windows;
using RetroRPG.components.menu;
using static RetroRPG.components.menu.Windows;

namespace RetroRPG
{
    /// <summary>
    /// Interaction logic for Menu_Load.xaml
    /// </summary>
    public partial class Menu_Load : Window
    {
        public Menu_Load()
        {
            InitializeComponent();
            Closed += OnWindowClosed;
        }
        private void OnWindowClosed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            WindowUtilities.RedirectWindow(this, typeof(MainWindow));
            this.Hide();
        }
    }
}
