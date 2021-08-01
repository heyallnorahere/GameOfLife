namespace GameOfLife.Frontends.Console
{
    internal sealed class ConsoleFrontend : Frontend
    {
        public static void Register()
        {
            RegisterFrontend("Console", () => new ConsoleFrontend());
        }
        private ConsoleFrontend() { }
        public override void Run()
        {
            IInputManager inputManager = new InputManager();
            IRenderer renderer = new Renderer();
            var game = new Game();
            CallSetupGameInstance(game);
            game.Run(inputManager, renderer);
        }
    }
}
