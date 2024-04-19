using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows;
using System.IO;
using System.Windows.Media;

namespace RetroRPG.components.dev
{
    public static class Grid
    {
        public static Border[] gridTiles = new Border[330];
        public static void CreateMapGrid() // INFO: "Tag" is correct. 22 * 15 = 330 and last tile is 330.
        {
            if (Map.OpenedMapPath == null || !Map.HasMapOpen)
            {
                Interaction.MsgBox("You do not have a map open.", MsgBoxStyle.OkOnly | MsgBoxStyle.Critical);
                return;
            }

            Map.CanvasWrapPanel.Children.Clear(); // wipe old content if it's here

            int rows = 22;
            int columns = 15;
            int raw_index = 0;

            string[] lines = File.ReadAllLines(Map.OpenedMapPath);
            for (int i = 0; i < rows; i++)
            {
                string[] tileIndices = lines[i].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);


                if (tileIndices.Length < columns)
                {
                    Array.Resize(ref tileIndices, columns);
                    for (int k = tileIndices.Length; k < columns; k++)
                    {
                        tileIndices[k] = "0";
                    }
                }

                for (int j = 0; j < columns; j++) // first to last
                {
                    int tileIndex;
                    if (int.TryParse(tileIndices[j], out tileIndex))
                    {
                        gridTiles[raw_index] = new Border
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
                        string identifier = $"Tile{tileIndex}";
                        Image tileImage = new Image
                        {
                            Source = new BitmapImage(new Uri($"pack://application:,,,/assets/sprites/tiles/tile{tileIndex:D3}.png")),
                            Name = identifier.ToString(),
                            Tag = raw_index, 
                            Width = 37.5,
                            Height = 37.5,
                            Opacity = 1.0
                        };

                        stackPanel.Children.Add(tileImage);

                        gridTiles[raw_index].MouseLeftButtonDown += (sender, e) =>
                        {
                            int previous_tile_id = Selection.Grid.HasGridSelected();
                            int selected_tile_id = (int)tileImage.Tag;

                            if (previous_tile_id != -1 && previous_tile_id != selected_tile_id)
                            {
                                Selection.Grid.ResetGrid();
                            }
                            if (previous_tile_id != selected_tile_id)
                            {
                                Selection.Grid.SelectGrid(selected_tile_id, stackPanel, tileImage);
                            }
                        };
                        gridTiles[raw_index].Child = stackPanel;

                        Map.CanvasWrapPanel.Children.Add(gridTiles[raw_index]);
                        raw_index++;
                    }
                    else
                    {
                        MessageBox.Show($"Error reading map file: Invalid tile index at row {i}, column {j}");
                    }
                }
            }
        }
    }
}
