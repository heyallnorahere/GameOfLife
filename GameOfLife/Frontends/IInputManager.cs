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
    public interface IInputManager
    {
        bool GetKey(Key key);
        bool this[Key key] { get; }
        void Update();
    }
}
