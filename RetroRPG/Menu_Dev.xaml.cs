using System.Windows;
using System.Windows.Controls;
using RetroRPG.components.dev;
using Menu = RetroRPG.components.dev.Menu;

namespace RetroRPG
{
    /// <summary>
    /// Interaction logic for Menu_Dev.xaml
    /// </summary>
    public partial class Menu_Dev : Window
    {

        public Menu_Dev()
        {
            InitializeComponent();
            Closed += OnWindowClosed;

            Tiles.SetupTiles(TileWrapper);
            Map.SetupMap(GridWrapper, LblWarning);

            Menu.SetupMenu(MenuBar);
        }
        private void OnWindowClosed(object sender, System.EventArgs e)
        {
            Application.Current.Shutdown();
        }


        /*
            TODO:
                - Add undo and redo functionality.
                - Add system where you select a sprite, then you select a grid tile to replace it.
        */
    }
}