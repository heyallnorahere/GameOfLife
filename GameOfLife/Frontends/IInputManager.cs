using System.Runtime.InteropServices;

namespace GameOfLife.Frontends
{
    public enum InputAction
    {
        FPSUp,
        FPSDown,
        PreviousFrame,
        NextFrame,
        Pause,
        Quit,
        PanUp,
        PanLeft,
        PanDown,
        PanRight,
        ZoomOut,
        ZoomIn
    }
    public interface IInputManager
    {
        bool IsInputHeld(InputAction key);
        bool this[InputAction key] { get; }
        void Update();
    }
}
