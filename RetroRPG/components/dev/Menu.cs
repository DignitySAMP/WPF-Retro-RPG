using Microsoft.VisualBasic;
using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Controls;

namespace RetroRPG.components.dev
{
    public static class Menu
    {
 
        public static void SetupMenu(MenuItem menuBar)
        {
            MenuItem menuNew = new MenuItem
            {
                Name = "MenuNew",
                Header = "New"
            };
            menuNew.Click += MenuNew_Click;

            MenuItem menuExport = new MenuItem
            {
                Name = "MenuExport",
                Header = "Save"
            };
            menuExport.Click += MenuExport_Click;

            MenuItem menuOpen = new MenuItem
            {
                Name = "MenuOpen",
                Header = "Open"
            };
            menuOpen.Click += MenuOpen_Click;


            MenuItem menuClose = new MenuItem
            {
                Name = "MenuClose",
                Header = "Close"
            };
            menuClose.Click += MenuClose_Click;

            MenuItem menuExit = new MenuItem
            {
                Name = "MenuExit",
                Header = "Exit"
            };
            menuExit.Click += MenuExit_Click;

            menuBar.Items.Add(menuNew);
            menuBar.Items.Add(menuExport);
            menuBar.Items.Add(menuOpen);
            menuBar.Items.Add(menuClose);
            menuBar.Items.Add(menuExit);
        }
        public static void MenuNew_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.Title = "Choose path and name of new map file";
            saveFileDialog.Filter = "Text files (*.txt)|*.txt";
            saveFileDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;

            bool? result = saveFileDialog.ShowDialog();
            if (result == true)
            {
                Map.NewMap(saveFileDialog);
            }
        }

        public static void MenuOpen_Click(object sender, RoutedEventArgs e)
        {
            Map.OpenMap();
        }

        public static void MenuExport_Click(object sender, RoutedEventArgs e)
        {
            Map.ExportMap();
        }

        public static void MenuClose_Click(object sender, RoutedEventArgs e)
        {
            Map.CloseMap();
        }


        public static void MenuExit_Click(object sender, RoutedEventArgs e)
        {
            MsgBoxResult result = Interaction.MsgBox("Your map will not be saved. You need to manually save it.", MsgBoxStyle.YesNo | MsgBoxStyle.Exclamation, "Warning: before you close");

            if (result == MsgBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }
    }
}
