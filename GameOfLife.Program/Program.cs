using System;
using System.Collections.Generic;
using GameOfLife.Frontends;
using CommandLine;

namespace GameOfLife.Program
{
    public class Program
    {
        public class Options
        {
            [Option('f', "frontend", HelpText = "Change the frontend running this application", Default = "Console")]
            public string? Frontend { get; set; }
            [Option('c', "config", HelpText = "Change the config file that this application reads from", Default = "config.json")]
            public string? ConfigFile { get; set; }
        }
        public static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args).WithParsed(OnParsed).WithNotParsed(OnError);
            var frontend = Frontend.Create(mFrontend ?? throw new NullReferenceException());
            frontend.SetupGameInstance += (Game instance) =>
            {
                Config.Load(mConfigFile ?? throw new NullReferenceException());
                instance.Board.LoadConfig();
            };
            frontend.Run();
        }
        private static void OnParsed(Options options)
        {
            mFrontend = options.Frontend;
            mConfigFile = options.ConfigFile;
        }
        private static void OnError(IEnumerable<Error> errors)
        {
            throw new NullReferenceException();
        }
        private static string? mFrontend, mConfigFile;
    }
}
