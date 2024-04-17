using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static RetroRPG.components.menu.Windows;

namespace RetroRPG
{
    /// <summary>
    /// Interaction logic for Menu_About.xaml
    /// </summary>
    public partial class Menu_About : Window
    {
        public Menu_About()
        {
            InitializeComponent();

            AudioManager.StopMenuMusic();
            CreateMapTiles();

            Closed += OnWindowClosed;
        }
        private void OnWindowClosed(object sender, System.EventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void CreateMapTiles()
        {
            // Add a vertical and horizontal scrollviewer. Then put it in a viewbox.
            // INFO: "Tag" is correct. 22 * 15 = 330 and last tile is 330.
            int rows = 22;
            int columns = 15;

            string[] lines = File.ReadAllLines("map.txt");

            int raw_index = 0; 

            for (int i = 0; i < rows; i++)
            {
                string[] tileIndices = lines[i].Split(',').Select(x => x.Trim()).ToArray();

                for (int j = 0; j < columns && j < tileIndices.Length; j++)
                {
                    int tileIndex;
                    if (int.TryParse(tileIndices[j], out tileIndex))
                    {
                        StackPanel stackPanel = new StackPanel
                        {
                            Width = 37.5,
                            Height = 37.5
                        };
                        Image tileImage = new Image
                        {
                            Source = new BitmapImage(new Uri($"pack://application:,,,/assets/sprites/tiles/tile{tileIndex:D3}.png")),
                            Tag = raw_index, // Use Tag property to associate tileIndex
                            Width = 37.5,
                            Height = 37.5,
                            Opacity = 1.0
                        };

                        stackPanel.Children.Add(tileImage);
                        raw_index++;


                        stackPanel.MouseLeftButtonDown += (sender, e) =>
                        {

                            string content = tileImage.Tag.ToString();

                            MessageBox.Show($"Button with content '{content}' was clicked!");
                        };


                        StackMap.Children.Add(stackPanel);
                    }
                    else
                    {
                        MessageBox.Show($"Invalid tile index at row {i}, column {j}");
                    }
                }
            }
        }
        private void ExportMap_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder mapBuilder = new StringBuilder();

            // Build the map string
            for (int i = 0; i < 22; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    // Assuming you have access to the StackMap children
                    if (StackMap.Children[i * 15 + j] is StackPanel stackPanel)
                    {
                        // Assuming the content of each child is a button with a number
                        if (stackPanel.Children[0] is Image tileImage)
                        {
                            // Extract the tile index from the image source URI
                            string sourceUri = tileImage.Source.ToString();
                            int startIndex = sourceUri.LastIndexOf("tile") + 4;
                            int endIndex = sourceUri.LastIndexOf(".png");
                            string tileIndexStr = sourceUri.Substring(startIndex, endIndex - startIndex);

                            mapBuilder.Append(tileIndexStr);
                        }
                        else
                        {
                            // If the child is not an image, append a placeholder
                            mapBuilder.Append("0");
                        }
                    }
                    else
                    {
                        // If the child is missing, append a placeholder
                        mapBuilder.Append("0");
                    }

                    // Append comma after each tile index
                    mapBuilder.Append(",");

                    // Append newline after each row
                    if (j == 14)
                    {
                        mapBuilder.AppendLine();
                    }
                }
            }

            // Write the map data to a file
            File.WriteAllText("map.txt", mapBuilder.ToString());

            MessageBox.Show("Map exported successfully to map.txt!");
        }

    }
}
