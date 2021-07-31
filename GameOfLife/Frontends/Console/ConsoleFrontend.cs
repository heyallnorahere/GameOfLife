namespace GameOfLife.Frontends.Console
{
    internal class ConsoleFrontend : Frontend
    {
        public ConsoleFrontend()
        {
            mInputManager = new InputManager();
        }
        public override IInputManager InputManager => mInputManager;
        public override IRenderer Renderer => throw new System.NotImplementedException();
        private InputManager mInputManager;
    }
}
