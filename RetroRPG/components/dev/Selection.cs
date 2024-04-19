using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace RetroRPG.components.dev
{
    public static class Selection
    {
        public static class Tiles
        {
            public static int tile_image_id = -1;
            public static StackPanel tile_image_stack = null;
            public static Image tile_image_img = null;

            public static void SelectTile(int tile, StackPanel stackpanel, Image image)
            {
                tile_image_id = tile;
                tile_image_stack = stackpanel;
                tile_image_img = image;


                tile_image_img.Opacity = 0.5;
                tile_image_stack.Background = Brushes.Red;
            }

            public static void ResetTile()
            {

                tile_image_img.Opacity = 1;
                tile_image_stack.Background = Brushes.Gray;

                tile_image_id = -1;
                tile_image_img = null;
                tile_image_stack = null;
            }


            public static int HasTileSelected()
            {
                return tile_image_id;
            }
        }

        public static class Grid
        {
            public static int grid_image_id = -1;
            public static StackPanel grid_image_stack = null;
            public static Image grid_image_img = null;

            public static void SelectGrid(int tile, StackPanel stackpanel, Image image)
            {
                if(Tiles.HasTileSelected() == -1)
                {
                    MessageBox.Show("You need to select a tile first.");
                    return;
                }

                grid_image_id = tile;
                grid_image_stack = stackpanel;
                grid_image_img = image;


                grid_image_img.Opacity = 0.5;
                grid_image_stack.Background = Brushes.Red;

                ReplaceTile();
            }

            public static void ResetGrid()
            {

                grid_image_img.Opacity = 1;
                grid_image_stack.Background = Brushes.Gray;

                grid_image_id = -1;
                grid_image_img = null;
                grid_image_stack = null;
            }


            public static int HasGridSelected()
            {
                return grid_image_id;
            }
        }

        public static void ReplaceTile()
        {
            Selection.Grid.grid_image_id = Tiles.HasTileSelected();
            Selection.Grid.grid_image_stack.Children.Clear();
            int identifier = Selection.Grid.grid_image_id;
            Selection.Grid.grid_image_img = new Image
            {
                Source = new BitmapImage(new Uri($"pack://application:,,,/assets/sprites/tiles/tile{identifier:D3}.png")),
                Name = $"Tile{identifier}",
                Tag = identifier,
                Width = 37.5,
                Height = 37.5,
                Opacity = 1.0
            };
            Selection.Grid.grid_image_stack.Children.Add(Selection.Grid.grid_image_img);
        }

    }
}
