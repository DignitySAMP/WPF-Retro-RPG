using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace RetroRPG.components.game
{
    internal class Actions
    {
        public static StackPanel actionRowStackPanel; // container for action buttons
        public static Button selectedButton = null; // cross-tracks selected button (see Canvas.cs)

        public static void CreateActionPanel(StackPanel stacker)
        {
            actionRowStackPanel = stacker;
            CreateActionsPanel();
        }

        public static void CreateActionsPanel()
        {
            actionRowStackPanel.Children.Clear();

            WrapPanel wrapper = new WrapPanel
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Background = Brushes.Black
            };

            wrapper.Resources.Add(typeof(Button), CreateButtonStyle());

            string[] buttonTexts = { "Examine", "Pickup", "Use", "Talk", "Attack", "Options" };
            foreach (string text in buttonTexts)
            {
                Button button = new Button { Content = text };
                button.Click += Button_Click;
                wrapper.Children.Add(button);
            }

            actionRowStackPanel.Children.Add(wrapper);
        }

        private static void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Button clickedButton = (Button)sender;

            if (selectedButton == clickedButton)
            {
                selectedButton = null;
                CreateActionsPanel();
            }

            else if (selectedButton != null)
            {
                History log = new History(gameViewInstance.TxtHistory);
                log.AddNewLine();
                log.AddTimeStamp(DateTime.Now, "#AAAAAA");
                log.AddLog("You must deselect your current button first", "#AA0000");
                log.AddDivider();
            }

            else if (selectedButton == null)
            {

                selectedButton = clickedButton;
                selectedButton.Background = Brushes.Red;
            }
        }

        private static Style CreateButtonStyle()  // handles styling for action buttons
        { 
            // store style
            Style buttonStyle = new Style(typeof(Button));
            buttonStyle.Setters.Add(new Setter(Button.BackgroundProperty, RetroColorConverter.ToBrush(RetroColor.Blue)));
            buttonStyle.Setters.Add(new Setter(Button.BorderBrushProperty, new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00000000"))));
            buttonStyle.Setters.Add(new Setter(Button.FontFamilyProperty,  (FontFamily)Application.Current.Resources["BodyFont"]));
            buttonStyle.Setters.Add(new Setter(Button.FontSizeProperty, 13.0));
            buttonStyle.Setters.Add(new Setter(Button.ForegroundProperty, Brushes.White));
            buttonStyle.Setters.Add(new Setter(Button.WidthProperty, 85.0));
            buttonStyle.Setters.Add(new Setter(Button.HeightProperty, 43.0));

            // create controltemplate
            ControlTemplate template = new ControlTemplate(typeof(Button));
            var borderFactory = new FrameworkElementFactory(typeof(Border));
            borderFactory.SetBinding(Border.BackgroundProperty, new Binding("Background") { RelativeSource = new RelativeSource(RelativeSourceMode.TemplatedParent) });
            borderFactory.SetBinding(Border.BorderBrushProperty, new Binding("BorderBrush") { RelativeSource = new RelativeSource(RelativeSourceMode.TemplatedParent) });
            borderFactory.SetValue(Border.BorderThicknessProperty, new Thickness(1));
            borderFactory.SetValue(Border.CornerRadiusProperty, new CornerRadius(0));

            // presenterfactory for borderfactory
            var contentPresenterFactory = new FrameworkElementFactory(typeof(ContentPresenter));
            contentPresenterFactory.SetBinding(ContentPresenter.HorizontalAlignmentProperty, new Binding("HorizontalAlignment") { RelativeSource = new RelativeSource(RelativeSourceMode.TemplatedParent) });
            contentPresenterFactory.SetBinding(ContentPresenter.VerticalAlignmentProperty, new Binding("VerticalAlignment") { RelativeSource = new RelativeSource(RelativeSourceMode.TemplatedParent) });
            
            borderFactory.AppendChild(contentPresenterFactory);

            // store template and assign it to the button style
            template.VisualTree = borderFactory;
            template.Triggers.Add(new Trigger { Property = UIElement.IsMouseOverProperty, Value = true, Setters = { new Setter(Button.BackgroundProperty, new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF4F4FCE"))) } });
            buttonStyle.Setters.Add(new Setter(Button.TemplateProperty, template));

            return buttonStyle;
        }

        private static GameView gameViewInstance; 
        public static void SetGameViewInstance(GameView gameView)
        {
            gameViewInstance = gameView;
        }
    }
}
