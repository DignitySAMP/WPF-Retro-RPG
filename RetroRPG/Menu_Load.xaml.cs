using System.Windows;
using RetroRPG.components.menu;

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
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            Windows.RedirectWindow(this, typeof(MainWindow));
            this.Hide();
        }
    }
}
