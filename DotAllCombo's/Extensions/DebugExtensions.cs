namespace DotAllCombo.Extensions
{
    using Ensage;
    using System;

    class DebugExtensions
    {
        public static void PrintError(ErrorExtensions.Error error)
        {
            Print.Error(error + " - #" + (int)error);
        }

        public class Chat
        {
            public static void PrintError(string text, params object[] arguments)
            {
                try
                {
                    Game.PrintMessage("<font color='#ff0000'>" + text + "</font>", MessageType.LogMessage);
                }
                catch
                {
                    ErrorExtensions.ThrowError(ErrorExtensions.Error.PRINTING_ERROR);
                }
            }

            public static void PrintSuccess(string text, params object[] arguments)
            {
                try
                { 
                    Game.PrintMessage("<font color='#00ff00'>" + text + "</font>", MessageType.LogMessage);
                }
                catch
                {
                    ErrorExtensions.ThrowError(ErrorExtensions.Error.PRINTING_ERROR);
                }
            }

            public static void PrintInfo(string text, params object[] arguments)
            {
                try { 
                    Game.PrintMessage("<font color='#ffffff'>" + text + "</font>", MessageType.LogMessage);
                }
                catch
                {
                    ErrorExtensions.ThrowError(ErrorExtensions.Error.PRINTING_ERROR);
                }
            }
        }

        public static class Print
        {
            public static void Info(string text, params object[] arguments)
            {
                try
                {
                    Encolored(text, ConsoleColor.White, arguments);
                }
                catch
                {
                    ErrorExtensions.ThrowError(ErrorExtensions.Error.PRINTING_ERROR);
                }
            }

            public static void Success(string text, params object[] arguments)
            {
                try {
                    Encolored(text, ConsoleColor.Green, arguments);
                }
                catch
                {
                    ErrorExtensions.ThrowError(ErrorExtensions.Error.PRINTING_ERROR);
                }
            }

            public static void Error(string text, params object[] arguments)
            {
                try {
                    Encolored(text, ConsoleColor.Red, arguments);
                }
                catch
                {
                    ErrorExtensions.ThrowError(ErrorExtensions.Error.PRINTING_ERROR);
                }
            }

            public static void Encolored(string text, ConsoleColor color, params object[] arguments)
            {
                try {
                    var clr = Console.ForegroundColor;
                    Console.ForegroundColor = color;
                    Console.WriteLine(text, arguments);
                    Console.ForegroundColor = clr;
                }
                catch
                {
                    ErrorExtensions.ThrowError(ErrorExtensions.Error.PRINTING_ERROR);
                }
            }
        }
    }
}
