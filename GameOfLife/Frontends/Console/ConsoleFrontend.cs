namespace GameOfLife.Frontends.Console
{
    internal sealed class ConsoleFrontend : Frontend
    {
        public static void Register()
        {
            RegisterFrontend("Console", () => new ConsoleFrontend());
        }
        private ConsoleFrontend()
        {
            mInputManager = new InputManager();
            mRenderer = new Renderer();
        }
        public override IInputManager InputManager => mInputManager;
        public override IRenderer Renderer => mRenderer;
        private readonly InputManager mInputManager;
        private readonly Renderer mRenderer;
    }
}
