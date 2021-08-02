namespace GameOfLife.Frontends.Console
{
    [Frontend("Console")]
    internal sealed class ConsoleFrontend : Frontend
    {
        public ConsoleFrontend() { }
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
