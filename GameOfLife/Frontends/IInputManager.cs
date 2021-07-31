using System.Runtime.InteropServices;

namespace GameOfLife.Frontends
{
    public enum Key
    {
        UpArrow,
        DownArrow,
        LeftArrow,
        RightArrow,
        Spacebar,
        Q,
        W,
        A,
        S,
        D,
        OemMinus,
        OemPlus
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct KeyState
    {
        public bool Up { get; set; }
        public bool Down { get; set; }
        public bool Held { get; set; }
        public void Reset()
        {
            Up = false;
            Down = false;
            Held = false;
        }
    }
    public interface IInputManager
    {
        KeyState GetKey(Key key);
        KeyState this[Key key] { get; }
        void Update();
    }
}
