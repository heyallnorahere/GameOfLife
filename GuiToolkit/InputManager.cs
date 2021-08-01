using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Silk.NET.Input;

namespace GuiToolkit
{
    public struct KeyState
    {
        internal void Reset()
        {
            Up = false;
            Down = false;
            Held = false;
        }
        public bool Up { get; internal set; }
        public bool Down { get; internal set; }
        public bool Held { get; internal set; }
    }
    internal class InputState
    {
        public InputState()
        {
            KeyStates = new Dictionary<Key, KeyState>();
            Reset();
        }
        public void Reset()
        {
            foreach (var key in Enum.GetValues<Key>())
            {
                var keyState = new KeyState();
                keyState.Reset();
                KeyStates[key] = keyState;
            }
        }
        public Dictionary<Key, KeyState> KeyStates { get; private set; }
    }
    public sealed class InputManager
    {
        public InputManager(IInputContext inputContext, int states = 2)
        {
            mInputStates = new Queue<InputState>();
            for (int i = 0; i < states; i++)
            {
                mInputStates.Enqueue(new InputState());
            }
            mInputContext = inputContext;
        }
        internal void UpdateAndSwapStates()
        {
            InputState HEAD = mInputStates.Dequeue();
            InputState? lastHEAD = mHEAD;
            mHEAD = HEAD;
            foreach (var key in Enum.GetValues<Key>())
            {
                if (key == Key.Unknown)
                {
                    continue;
                }
                var keyState = mHEAD.KeyStates[key];
                for (int i = 0; i < mInputContext.Keyboards.Count; i++)
                {
                    var keyboard = mInputContext.Keyboards[i];
                    if (keyboard.IsKeyPressed(key) && !keyState.Held)
                    {
                        keyState.Held = true;
                    }
                }
                bool lastHeld = lastHEAD?.KeyStates[key].Held ?? false;
                keyState.Down = keyState.Held && !lastHeld;
                keyState.Up = !keyState.Held && lastHeld;
                mHEAD.KeyStates[key] = keyState;
            }
            mInputStates.Enqueue(mHEAD);
        }
        public KeyState GetKey(Key key)
        {
            return mHEAD?.KeyStates[key] ?? throw new NullReferenceException();
        }
        public KeyState this[Key key] => GetKey(key);
        private readonly IInputContext mInputContext;
        private readonly Queue<InputState> mInputStates;
        private InputState? mHEAD;
    }
}
