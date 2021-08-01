using GameOfLife.Frontends;

namespace GameOfLife.Program
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var frontend = Frontend.Create("GUI");
            frontend.SetupGameInstance += (Game instance) =>
            {
                Config.Load();
                instance.Board.LoadConfig();
            };
            frontend.Run();
        }
    }
}
