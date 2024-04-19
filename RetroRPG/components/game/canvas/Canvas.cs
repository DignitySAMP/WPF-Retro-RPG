using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using System;
using System.Collections.Generic;
using System.Windows.Media.Imaging;
using System.Linq;
using System.IO;

namespace RetroRPG.components.game
{
    public static class GameGrid
    {
        private static void TileFunction(int row, int column, string action)
        {
            if (action != null)
            {
                History log = new History(gameViewInstance.TxtHistory);
                log.AddNewLine();
                log.AddTimeStamp(DateTime.Now, "#AAAAAA");
                log.AddLog($"You are trying to ", "#FFFFFF");
                log.AddLog($"{action} ", "#00AAAA");
                log.AddLog($"tile ", "#FFFFFF");
                log.AddLog($"{row}-{column}.", "#AA00AA");
                log.AddDivider();
            }
            else
            {
                History log = new History(gameViewInstance.TxtHistory);
                log.AddNewLine();
                log.AddTimeStamp(DateTime.Now, "#AAAAAA");
                log.AddLog($"You have clicked on tile ", "#FFFFFF");
                log.AddLog($"{row}-{column}.", "#FF55FF");
                log.AddDivider();
            }
        }
        public static void CreateGameView(WrapPanel gameCanvas)
        {
            int rows = 22;
            int columns = 15;
            Random random = new Random();

            string[] lines = File.ReadAllLines("map.txt");

            for (int i = 0; i < rows; i++)
            {
                string[] tileIndices = lines[i].Split(',').Select(x => x.Trim()).ToArray();

                for (int j = 0; j < columns; j++)
                {
                    Border border = new Border
                    {
                        BorderBrush = Brushes.Gray,
                        BorderThickness = new Thickness(1),
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center
                    };

                    StackPanel stackPanel = new StackPanel
                    {
                        Width = 37.5,
                        Height = 37.5
                    };

                    int tileIndex = int.Parse(tileIndices[j]);

                    Image tileImage = new Image
                    {
                        Source = new BitmapImage(new Uri($"pack://application:,,,/assets/sprites/tiles/tile{tileIndex:D3}.png")),
                        Width = 37.5,
                        Height = 37.5,
                        Opacity = 1.0
                    };
                    stackPanel.Children.Add(tileImage);

                    Label indexLabel = new Label
                    {
                        Content = tileIndex.ToString(),
                        HorizontalContentAlignment = HorizontalAlignment.Center,
                        VerticalContentAlignment = VerticalAlignment.Center,
                        FontSize = 10
                    };
                    stackPanel.Children.Add(indexLabel);

                    border.Child = stackPanel;

                    Grid.SetRow(border, i);
                    Grid.SetColumn(border, j);
                    gameCanvas.Children.Add(border);

                    border.MouseEnter += (sender, e) =>
                    {
                        tileImage.Opacity = 0.3;
                    };

                    border.MouseLeave += (sender, e) =>
                    {
                        tileImage.Opacity = 1;
                    };

                    border.MouseLeftButtonDown += (sender, e) =>
                    {
                        TileFunction(Grid.GetRow(border), Grid.GetColumn(border), Actions.selectedButton?.Content.ToString());

                        if (Actions.selectedButton != null) // deselect button & reset styling
                        {
                            Actions.selectedButton = null;
                            Actions.CreateActionsPanel();
                        }
                    };
                }
            }
        }



        private static GameView gameViewInstance;
        public static void SetGameViewInstance(GameView gameView)
        {
            gameViewInstance = gameView;
        }
    }
}
