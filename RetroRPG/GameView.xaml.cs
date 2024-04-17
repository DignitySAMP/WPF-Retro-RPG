using System.Windows;
using static RetroRPG.components.game.GameGrid;
using static RetroRPG.components.game.Interface;
using static RetroRPG.components.game.History;
using static RetroRPG.components.game.Actions;
using RetroRPG.components.game;
using System;
using static RetroRPG.components.menu.Windows;

namespace RetroRPG
{
    /// <summary>
    /// Interaction logic for GameView.xaml
    /// </summary>
    public partial class GameView : Window
    {
        public GameView()
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
            CreateGameView(GameWrapper);
            CreateInventoryTiles(StackInterface);

            History log = new History(TxtHistory);
            log.AddTimeStampFormatted(DateTime.Now, "Starting game...", "#FFFFFF");
            log.AddDivider(); 
            log.AddNewLine();

            CreateHistory(TxtHistory); // debug
            CreateActionPanel(StackActionBtns);
        }
    }
}
