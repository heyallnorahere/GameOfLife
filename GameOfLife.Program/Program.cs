using GameOfLife.Frontends;

namespace GameOfLife.Program
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var game = new Game(Frontend.Create(FrontendType.Console));
            // todo: load state
            game.Run();
        }
    }
}
