using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace RetroRPG.components.game
{
    internal class History
    {
        private readonly TextBlock _textBlock;

        public History(TextBlock textBlock)
        {
            _textBlock = textBlock;
        }

        public static void CreateHistory(TextBlock textBlock)
        // Make this procedural based on quest progress.
        {
            History log = new History(textBlock);

            log.AddTimeStamp(DateTime.Now, "#AAAAAA");
            log.AddLog("Welcome to the bustling village of ", "#FFFFFF");
            log.AddLog("Rumpertshire", "#5555FF");
            log.AddLog(", located in ", "#FFFFFF");
            log.AddLog("England", "#0000AA");
            log.AddLog(".", "#FFFFFF");
            log.AddDivider();
            log.AddNewLine();
            log.AddTimeStamp(DateTime.Now, "#AAAAAA");
            log.AddLog("Enter the ", "#FFFFFF");
            log.AddLog("barn", "#55FF55");
            log.AddLog(" and speak to your ", "#FFFFFF");
            log.AddLog("father", "#FFFF55");
            log.AddLog(" to continue.", "#FFFFFF");
            log.AddDivider();
        }

        public void AddLog(string message, string color)
        {
            var run = new Run(message)
            {
                Foreground = (System.Windows.Media.Brush)new System.Windows.Media.BrushConverter().ConvertFromString(color)
            };
            _textBlock.Inlines.Add(run); 
            
            if (gameViewInstance != null && gameViewInstance.HistoryScroller != null)
            {
                gameViewInstance.HistoryScroller.ScrollToVerticalOffset(gameViewInstance.HistoryScroller.ScrollableHeight);
                gameViewInstance.HistoryScroller.ScrollToBottom();
            }
        }

        public void AddNewLine()
        {
            AddLog(Environment.NewLine, "#FFFFFF");
        }
        public void AddDivider()
        {
            AddNewLine();
            AddLog("------------------------", "#FFFFFF");
            AddNewLine();
        }

        public void AddTimeStamp(DateTime time, string color)
        {
            AddLog($"[{time.ToString("HH:mm:ss")}] ", color);
            AddLog("-----------------", "#FFFFFF");
        }

        public void AddTimeStampFormatted(DateTime time, string text, string color)
        {
            AddLog($"[{time.ToString("HH:mm:ss")}] ", "#AAAAAA");
            AddLog(text, color);
        }

        private static GameView gameViewInstance;
        public static void SetGameViewInstance(GameView gameView)
        {
            gameViewInstance = gameView;
        }
    }
}
