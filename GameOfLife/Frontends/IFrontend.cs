namespace GameOfLife.Frontends
{
    public interface IFrontend
    {
        IRenderer Renderer { get; }
        IInputManager InputManager { get; }
    }
}
