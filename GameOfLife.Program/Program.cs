using System;
using GameOfLife.Frontends;
using CommandLine;

namespace GameOfLife.Program
{
    public class Program
    {
        private struct Options
        {
            [Option('f', "frontend", Required = false, HelpText = "Change the frontend running this application")]
            public string? Frontend { get; set; }
        }
        static Program()
        {
            mFrontend = "Console";
        }
        public static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args).WithParsed(OnParsed);
            var frontend = Frontend.Create(mFrontend);
            frontend.SetupGameInstance += (Game instance) =>
            {
                Config.Load();
                instance.Board.LoadConfig();
            };
            frontend.Run();
        }
        private static void OnParsed(Options options)
        {
            if (options.Frontend != null)
            {
                mFrontend = options.Frontend;
            }
        }
        private static string mFrontend;
    }
}
