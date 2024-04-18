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
        /*
            TODO:
                - Add a file opener.
                - Make it so the exported map is put in an "exports" folder, using ORIGINAL_FILENAME_edited_DATE.txt.
                - Add a scrollviewer with the tiles.
                - Add system where you select a sprite, then you select a grid tile to replace it.
                - Make an undo button. 
        */
        Border[] gridTiles = new Border[330];
        private void CreateMapTiles()
        {
            // INFO: "Tag" is correct. 22 * 15 = 330 and last tile is 330.
            /*
                IMPORTANT: The loader/exporter WORKS. However, because of the dynamic nature of the wrappanel, the tiles will be different in the Canvas compared to the .txt.
                Only edit maps using this editor
            */
            int rows = 22;
            int columns = 15;
            int raw_index = 0;

            string[] lines = File.ReadAllLines("map.txt");
            for (int i = 0; i < rows; i++)
            {
                string[] tileIndices = lines[i].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                // Pad the array with zeros if it has fewer elements than 'columns'
                if (tileIndices.Length < columns)
                {
                    Array.Resize(ref tileIndices, columns);
                    for (int k = tileIndices.Length; k < columns; k++)
                    {
                        tileIndices[k] = "0";
                    }
                }
                // Iterate from first to last element
                for (int j = 0; j < columns; j++)
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
                            Tag = raw_index, // Use Tag property to associate tileIndex
                            Width = 37.5,
                            Height = 37.5,
                            Opacity = 1.0
                        };

                        stackPanel.Children.Add(tileImage);

                        gridTiles[raw_index].MouseLeftButtonDown += (sender, e) =>
                        {
                            string content = tileImage.Tag.ToString();
                            MessageBox.Show($"Button with content '{content}' was clicked!");
                        };
                        gridTiles[raw_index].Child = stackPanel;

                        StackMap.Children.Add(gridTiles[raw_index]);
                        raw_index++;
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
            int rows = 22;
            int columns = 15;

            StringBuilder mapBuilder = new StringBuilder();
            bool result = true;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    // Calculate the index of the tile in the gridTiles array
                    int index = i * columns + j;

                    if (gridTiles[index].Child is StackPanel stackpanel)
                    {
                        if (stackpanel.Children[0] is Image tileImage)
                        {
                            string formattedName = tileImage.Name.Replace("Tile", "");
                            mapBuilder.Append($"{formattedName}");

                            // Append a comma if it's not the last tile in the row
                            if (j < columns - 1)
                            {
                                mapBuilder.Append(", ");
                            }
                        }
                    }
                    else
                    {
                        // If the child is not an image, append a placeholder
                        mapBuilder.Append("0");

                        // Append a comma if it's not the last tile in the row
                        if (j < columns - 1)
                        {
                            mapBuilder.Append(", ");
                        }

                        result = false;
                    }
                }

                // Append a new line character after each row
                mapBuilder.AppendLine();
            }

            // Write the map data to a file
            File.WriteAllText("exported_map.txt", mapBuilder.ToString());

            if (result)
            {
                MessageBox.Show("Map exported successfully to exported_map.txt!");
            }
            else
            {
                MessageBox.Show("There were issues with exporting the map. Check the output file for missing or incorrect tiles.");
            }
        }


    }
}

/*
 This fixes bugged maps.


// Read the contents of the original text file
string[] lines = File.ReadAllLines("map.txt");

// Ensure each row has the correct number of elements
for (int i = 0; i < lines.Length; i++)
{
    string[] tileIndices = lines[i].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

    // Pad the row with zeros if it has fewer elements than 'columns'
    if (tileIndices.Length < columns)
    {
        Array.Resize(ref tileIndices, columns);
        for (int k = tileIndices.Length; k < columns; k++)
        {
            tileIndices[k] = "0";
        }
    }
    // Truncate the row if it has more elements than 'columns'
    else if (tileIndices.Length > columns)
    {
        Array.Resize(ref tileIndices, columns);
    }

    // Join the elements back into a comma-separated string
    lines[i] = string.Join(",", tileIndices);
}

// Join the rows into a single string with newline characters
string formattedContent = string.Join(Environment.NewLine, lines);

// Write the formatted content back to the text file
File.WriteAllText("map.txt", formattedContent, Encoding.UTF8);
*/