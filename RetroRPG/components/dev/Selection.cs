using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
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
                grid_image_id = tile;
                grid_image_stack = stackpanel;
                grid_image_img = image;


                grid_image_img.Opacity = 0.5;
                grid_image_stack.Background = Brushes.Red;
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

    }
}
