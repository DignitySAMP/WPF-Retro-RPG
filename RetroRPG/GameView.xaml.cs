using System.Windows;
using static RetroRPG.components.game.GameGrid;
using static RetroRPG.components.game.Interface;
using static RetroRPG.components.game.History;
using static RetroRPG.components.game.Actions;
using RetroRPG.components.game;
using System;
using System.Windows.Controls;

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
            GameGrid.SetGameViewInstance(this);
            CreateInventoryTiles(StackInterface);

            History log = new History(TxtHistory);
            log.AddTimeStampFormatted(DateTime.Now, "Starting game...", "#FFFFFF");
            log.AddDivider(); 
            log.AddNewLine();


            History.SetGameViewInstance(this);
            CreateHistory(TxtHistory); // debug

            Actions.SetGameViewInstance(this);
            CreateActionPanel(StackActionBtns);
        }
    }
}
