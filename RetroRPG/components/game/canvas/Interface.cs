using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;
using RetroRPG.components;

namespace RetroRPG.components.game
{
    public static class Interface
    {
        public static StackPanel interfacePanel;
        public static WrapPanel interfaceButtonRow;
        public static StackPanel interfacePanelContent; 
        public static Button selectedButton; // Store reference to the selected button


        public static void CreateInventoryTiles(StackPanel stacker)
        {
            interfacePanel = stacker;
            interfaceButtonRow = new WrapPanel
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            interfacePanel.Children.Add(interfaceButtonRow);

            CreateTab("Map", "/assets/img/interface/interface_map.png");
            CreateTab("Items", "/assets/img/interface/interface_items.png");
            CreateTab("Skills", "/assets/img/interface/interface_skills.png");
            CreateTab("Stats", "/assets/img/interface/interface_stats.png");

            interfacePanelContent = new StackPanel();
            interfacePanel.Background = RetroColorConverter.ToBrush(RetroColor.BrightCyan);
            interfacePanel.Children.Add(interfacePanelContent);
        }

        public static void CreateTab(string tabName, string imagePath)
        {
            Image image = new Image();
            image.Width = 65;
            image.Height = 32;
            image.Source = new BitmapImage(new Uri(imagePath, UriKind.Relative));

            Button tabButton = new Button();
            tabButton.Name = tabName;
            tabButton.Content = image;
            tabButton.Style = GetTabButtonStyle(tabName);
            tabButton.Click += TabButton_Click;

            interfaceButtonRow.Children.Add(tabButton);
        }

        private static Style GetTabButtonStyle(string tabName, bool selectedTab = false)
        {
            Style tabButtonStyle = new Style(typeof(Button));

            tabButtonStyle.Setters.Add(new Setter(Button.BorderThicknessProperty, new Thickness(0)));

            switch (tabName)
            {
                case "Map":
                    tabButtonStyle.Setters.Add(new Setter(Button.BackgroundProperty, RetroColorConverter.ToBrush(RetroColor.Cyan)));
                    break;
                case "Items":
                    tabButtonStyle.Setters.Add(new Setter(Button.BackgroundProperty, RetroColorConverter.ToBrush(RetroColor.Cyan)));
                    break;
                case "Skills":
                    tabButtonStyle.Setters.Add(new Setter(Button.BackgroundProperty, RetroColorConverter.ToBrush(RetroColor.Cyan)));
                    break;
                case "Stats":
                    tabButtonStyle.Setters.Add(new Setter(Button.BackgroundProperty, RetroColorConverter.ToBrush(RetroColor.Cyan)));
                    break;
                default: // Default style if tab name doesn't match
                    tabButtonStyle.Setters.Add(new Setter(Button.BackgroundProperty, RetroColorConverter.ToBrush(RetroColor.Cyan)));
                    break;
            }

            if (selectedTab) // if tab is selected, set color to cyan
            { 
                tabButtonStyle.Setters.Add(new Setter(Button.BackgroundProperty, RetroColorConverter.ToBrush(RetroColor.BrightCyan)));
            }

            Trigger mouseOverTrigger = new Trigger()
            {
                Property = UIElement.IsMouseOverProperty,
                Value = true
            };

            // Set the background color on hover
            mouseOverTrigger.Setters.Add(new Setter(Button.BackgroundProperty, Brushes.Red));
            ControlTemplate template = new ControlTemplate(typeof(Button));
            var border = new FrameworkElementFactory(typeof(Border));
            border.SetValue(Border.BackgroundProperty, new TemplateBindingExtension(Button.BackgroundProperty));
            border.AppendChild(new FrameworkElementFactory(typeof(ContentPresenter)));
            template.VisualTree = border;
            template.Triggers.Add(mouseOverTrigger);
            tabButtonStyle.Setters.Add(new Setter(Control.TemplateProperty, template));

            return tabButtonStyle;
        }
        public static void TabButton_Click(object sender, RoutedEventArgs e)
        {
            Button tabButton = sender as Button;

            if (selectedButton != null) // Set previous button to default
            {
                selectedButton.Style = GetTabButtonStyle(selectedButton.Name, false);
            }

            selectedButton = tabButton; // update selected button
            selectedButton.Style = GetTabButtonStyle(tabButton.Name, true);

            interfacePanelContent.Children.Clear();
            switch (tabButton.Name)
            {
                case "Map":
                    interfacePanelContent.Children.Add(new Label() { FontFamily = (FontFamily)Application.Current.Resources["BodyFont"], Content = "Map Content" }); ;
                    break;
                case "Items":
                    interfacePanelContent.Children.Add(new Label() { FontFamily = (FontFamily)Application.Current.Resources["BodyFont"], Content = "Items Content" });
                    break;
                case "Skills":
                    interfacePanelContent.Children.Add(new Label() { FontFamily = (FontFamily)Application.Current.Resources["BodyFont"], Content = "Skills Content" });
                    break;
                case "Stats":
                    interfacePanelContent.Children.Add(new Label() { FontFamily = (FontFamily)Application.Current.Resources["BodyFont"], Content = "Stats Content" });
                    break;
                default:
                    break;
            }
        }


    }

}
