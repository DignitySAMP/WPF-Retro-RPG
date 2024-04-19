using Microsoft.VisualBasic;
using System;
using System.IO;
using System.Text;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace RetroRPG.components.dev
{
    public static class Tiles
    {
        static UniformGrid gridWrapper = null;
        public static void SetupTiles(UniformGrid grid)
        {
            gridWrapper = grid;

            CreateTiles();
        }

        public static void CreateTiles()
        {
            string basePath = "/assets/sprites/tiles/";
            int tilesPerRow = 8;

            for (int i = 0; i < 255; i++)
            {
                string imagePath = $"{basePath}tile{i.ToString("000")}.png";
                Image image = new Image
                {
                    Source = new BitmapImage(new Uri(imagePath, UriKind.Relative)),
                    Width = 28,
                    Height = 28,
                    Tag = i
                };
                Console.WriteLine($"Tag: {i}");

                StackPanel stackpanel = new StackPanel
                {
                    Width = 28,
                    Height = 28
                };

                stackpanel.Children.Add(image);

                image.MouseLeftButtonUp += (sender, e) =>
                {
                    int previous_tile_id = Selection.Tiles.HasTileSelected();
                    int selected_tile_id = (int)image.Tag;

                    if (previous_tile_id != -1)
                    {
                        Selection.Tiles.ResetTile();
                    }
                    if (previous_tile_id != selected_tile_id)
                    {
                        Selection.Tiles.SelectTile(selected_tile_id, stackpanel, image);
                    }
                };

                gridWrapper.Children.Add(stackpanel);

                // Check if we need to start a new row
                if (gridWrapper.Children.Count % tilesPerRow == 0)
                {
                    gridWrapper.Rows++;
                }
            }
        }
    }
}
