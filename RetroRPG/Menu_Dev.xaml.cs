using Microsoft.Win32;
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
using Microsoft.VisualBasic;
using static RetroRPG.components.menu.Windows;

namespace RetroRPG
{
    /// <summary>
    /// Interaction logic for Menu_Dev.xaml
    /// </summary>
    public partial class Menu_Dev : Window
    {

        bool HasMapOpen = false;
        string OpenMapPath = null;
        public Menu_Dev()
        {
            InitializeComponent();
            AudioManager.StopMenuMusic();
            Closed += OnWindowClosed;
            PopulateTiles();
        }
        private void OnWindowClosed(object sender, System.EventArgs e)
        {
            Application.Current.Shutdown();
        }


        /*
            TODO:
                - Add undo and redo functionality.
                - Add system where you select a sprite, then you select a grid tile to replace it.
        */
        Border[] gridTiles = new Border[330];
        private void CreateMapTiles() // INFO: "Tag" is correct. 22 * 15 = 330 and last tile is 330.
        {
            if (OpenMapPath == null || !HasMapOpen)
            {
                Interaction.MsgBox("You do not have a map open.", MsgBoxStyle.OkOnly | MsgBoxStyle.Critical);
                return;
            }

            StackMap.Children.Clear(); // wipe old content if it's here

            int rows = 22;
            int columns = 15;
            int raw_index = 0;

            string[] lines = File.ReadAllLines(OpenMapPath);
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



        private void MenuNew_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.Title = "Choose path and name of new map file";
            saveFileDialog.Filter = "Text files (*.txt)|*.txt";
            saveFileDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;

            bool? result = saveFileDialog.ShowDialog();
            if (result == true)
            {

                int rows = 22;
                int columns = 15;

                StringBuilder mapBuilder = new StringBuilder();

                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        int index = i * columns + j;

                        mapBuilder.Append("0");

                        if (j < columns - 1)
                        {
                            mapBuilder.Append(", ");
                        }

                    }

                    mapBuilder.AppendLine();
                }

                File.WriteAllText(saveFileDialog.FileName, mapBuilder.ToString());
                Interaction.MsgBox($"You have created a new map at location:\n{saveFileDialog.FileName}", MsgBoxStyle.Information);
                HasMapOpen = true;
                OpenMapPath = saveFileDialog.FileName;
                CreateMapTiles();
            }
        }

        private void MenuOpen_Click(object sender, RoutedEventArgs e)
        {
            if (!HasMapOpen)
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();

                openFileDialog.Title = "Select a map file to open (*.txt)";
                openFileDialog.Filter = "Text files (*.txt)|*.txt";
                openFileDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;

                bool? result = openFileDialog.ShowDialog();
                if (result == true)
                {
                    LblWarning.Visibility = Visibility.Hidden;
                    MenuOpen.Header = "Close";

                    HasMapOpen = true;
                    OpenMapPath = openFileDialog.FileName;

                    CreateMapTiles();

                }
            }
            else if (HasMapOpen)
            {
                MenuOpen.Header = "Open";
                HasMapOpen = false;
                OpenMapPath = null;

                StackMap.Children.Clear();
                LblWarning.Visibility = Visibility.Visible;
            }
        }

        private void MenuExport_Click(object sender, RoutedEventArgs e)
        {
            if (OpenMapPath == null || !HasMapOpen)
            {
                Interaction.MsgBox("You do not have a map open.", MsgBoxStyle.OkOnly | MsgBoxStyle.Critical);
                return;
            }
            int rows = 22;
            int columns = 15;

            StringBuilder mapBuilder = new StringBuilder();
            bool result = true;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    int index = i * columns + j;

                    if (gridTiles[index].Child is StackPanel stackpanel)
                    {
                        if (stackpanel.Children[0] is Image tileImage)
                        {
                            string formattedName = tileImage.Name.Replace("Tile", "");
                            mapBuilder.Append($"{formattedName}");

                            if (j < columns - 1)
                            {
                                mapBuilder.Append(", ");
                            }
                        }
                    }
                    else
                    {
                        mapBuilder.Append("0");

                        if (j < columns - 1)
                        {
                            mapBuilder.Append(", ");
                        }

                        result = false;
                    }
                }

                mapBuilder.AppendLine();
            }

            File.WriteAllText(OpenMapPath, mapBuilder.ToString());

            if (result)
            {
                MessageBox.Show("Map exported successfully to exported_map.txt!");
            }
            else
            {
                MessageBox.Show("There were issues with exporting the map. Check the output file for missing or incorrect tiles.");
            }
        }

        private void MenuRefactor_Click(object sender, RoutedEventArgs e)
        {
            if (OpenMapPath == null || !HasMapOpen)
            {
                Interaction.MsgBox("You do not have a map open.", MsgBoxStyle.OkOnly | MsgBoxStyle.Critical);
                return;
            }

            MsgBoxResult result = Interaction.MsgBox("This will resort your entire map. It may stop working.\n\nDo you want to continue?", MsgBoxStyle.YesNo | MsgBoxStyle.Exclamation, "Warning");

            if (result == MsgBoxResult.Yes)
            {
                int columns = 15;

                string[] lines = File.ReadAllLines(OpenMapPath);

                for (int i = 0; i < lines.Length; i++)
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
                    else if (tileIndices.Length > columns)
                    {
                        Array.Resize(ref tileIndices, columns);
                    }

                    lines[i] = string.Join(",", tileIndices);
                }

                string formattedContent = string.Join(Environment.NewLine, lines);

                File.WriteAllText(OpenMapPath, formattedContent, Encoding.UTF8); ;
            }
        }


        private void MenuClose_Click(object sender, RoutedEventArgs e)
        {
            MsgBoxResult result = Interaction.MsgBox("Your map will not be saved. You need to manually save it.", MsgBoxStyle.YesNo | MsgBoxStyle.Exclamation, "Warning: before you close");

            if (result == MsgBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }

        private void PopulateTiles()
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

                image.MouseLeftButtonUp += (sender, e) =>
                {

                    int imageNumber = (int)image.Tag;
                    MessageBox.Show($"Clicked on image {imageNumber}");

                };

                TileWrapper.Children.Add(image);

                // Check if we need to start a new row
                if (TileWrapper.Children.Count % tilesPerRow == 0)
                {
                    TileWrapper.Rows++;
                }
            }
        }

    }
}