using Microsoft.VisualBasic;
using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows;
using System.IO;
using System.Text;
using Microsoft.Win32;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace RetroRPG.components.dev
{
    public static class Map
    {
        public static WrapPanel CanvasWrapPanel = new WrapPanel();
        public static Label CanvasWarningLabel = new Label();
        public static void SetupMap(WrapPanel wrap, Label label)
        {
            CanvasWrapPanel = wrap;
            CanvasWarningLabel = label;
        }

        public static bool HasMapOpen = false;
        public static string OpenedMapPath = null;

        public static void OpenMap()
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
                    CanvasWarningLabel.Visibility = Visibility.Hidden;

                    HasMapOpen = true;
                    OpenedMapPath = openFileDialog.FileName;

                    Grid.CreateMapGrid();
                }
            }
            else
            {
                Interaction.MsgBox("You already have a map open. Close it first.", MsgBoxStyle.OkOnly | MsgBoxStyle.Critical);
            }
        }

        public static void ExportMap()
        {
            if (OpenedMapPath == null || !HasMapOpen)
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

                    if (Grid.gridTiles[index].Child is StackPanel stackpanel)
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

            File.WriteAllText(OpenedMapPath, mapBuilder.ToString());

            if (result)
            {
                MessageBox.Show("Map exported successfully to exported_map.txt!");
            }
            else
            {
                MessageBox.Show("There were issues with exporting the map. Check the output file for missing or incorrect tiles.");
            }
        }

        public static void NewMap(SaveFileDialog result )
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

            File.WriteAllText(result.FileName, mapBuilder.ToString());
            Interaction.MsgBox($"You have created a new map at location:\n{result.FileName}", MsgBoxStyle.Information);

            // Create the map without the dialog.
            CanvasWarningLabel.Visibility = Visibility.Hidden;

            HasMapOpen = true;
            OpenedMapPath = result.FileName;

            Grid.CreateMapGrid();
        }

        public static void CloseMap()
        {
            if (HasMapOpen)
            {
                MsgBoxResult result = Interaction.MsgBox("Do you wish to save before closing?", MsgBoxStyle.YesNo | MsgBoxStyle.Exclamation, "Warning");

                if (result == MsgBoxResult.Yes)
                {
                    ExportMap();
                }

                HasMapOpen = false;
                OpenedMapPath = null;

                CanvasWrapPanel.Children.Clear();
                CanvasWarningLabel.Visibility = Visibility.Visible;
            }
            else
            {
                Interaction.MsgBox("You do not have a map open.", MsgBoxStyle.OkOnly | MsgBoxStyle.Critical);
            }
        }

        public static void ResortMap()
        { // Put this under "options" when you make it dynamic.
            if (Map.OpenedMapPath == null || !Map.HasMapOpen)
            {
                Interaction.MsgBox("You do not have a map open.", MsgBoxStyle.OkOnly | MsgBoxStyle.Critical);
                return;
            }

            MsgBoxResult result = Interaction.MsgBox("This will resort your entire map. It may stop working.\n\nDo you want to continue?", MsgBoxStyle.YesNo | MsgBoxStyle.Exclamation, "Warning");

            if (result == MsgBoxResult.Yes)
            {
                int columns = 15;

                string[] lines = File.ReadAllLines(Map.OpenedMapPath);

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

                File.WriteAllText(Map.OpenedMapPath, formattedContent, Encoding.UTF8);
            }
        }

    }
}
