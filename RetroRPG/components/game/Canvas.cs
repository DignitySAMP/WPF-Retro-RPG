using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using System;
using System.Collections.Generic;

namespace RetroRPG.components.game
{
    public static class GameGrid
    {
        private static Dictionary<Border, Brush> originalTileColors = new Dictionary<Border, Brush>();

        private static void TileFunction(int row, int column, string action)
        {
            if (action != null)
            {
                Console.WriteLine($"Tile clicked: Row {row}, Column {column}, Action: {action}");
            }
            else
            {
                Console.WriteLine($"Tile clicked WITHOUT action: Row {row}, Column {column}");
            }
        }

        public static void CreateGameView(WrapPanel gameCanvas)
        {
            int rows = 22;
            int columns = 15;

            for (int i = 0; i < rows; i++)
            {
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
                        Background = (i + j) % 2 == 0 ? new SolidColorBrush(Color.FromRgb(0, 170, 0)) : new SolidColorBrush(Color.FromRgb(85, 255, 85)),
                        Width = 37.5,
                        Height = 37.5
                    };

                    border.Child = stackPanel;

                    Grid.SetRow(border, i);
                    Grid.SetColumn(border, j);
                    gameCanvas.Children.Add(border);

                    originalTileColors.Add(border, stackPanel.Background); // store original color

                    border.MouseEnter += (sender, e) =>
                    {
                        stackPanel.Background = Brushes.LightGray;
                    };

                    border.MouseLeave += (sender, e) =>
                    {
                        stackPanel.Background = originalTileColors[(Border)sender];
                    };

                    border.MouseLeftButtonDown += (sender, e) =>
                    {
                        TileFunction(Grid.GetRow(border), Grid.GetColumn(border), Actions.selectedButton?.Content.ToString());

                        if (Actions.selectedButton != null) // deselect button & reset styling
                        {
                            Actions.selectedButton = null;
                            Actions.CreateActionsPanel();
                        }
                        stackPanel.Background = originalTileColors[(Border)sender]; // restore color of tile
                    };
                }
            }
        }
    }
}
