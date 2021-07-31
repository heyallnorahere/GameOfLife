using System;
using System.Collections.Generic;

namespace GameOfLife.Frontends.Console
{
    internal class InputManager : IInputManager
    {
        public InputManager()
        {
            mKeyStates = new();
        }
        public KeyState this[Key key] => GetKey(key);
        public KeyState GetKey(Key key)
        {
            return mKeyStates[key];
        }
        public void Update()
        {
            var keys = new List<ConsoleKey>();
            while (System.Console.KeyAvailable)
            {
                var keyInfo = System.Console.ReadKey(true);
                keys.Add(keyInfo.Key);
            }
            var lastValues = mKeyStates;
            mKeyStates = new();
            foreach (var key in Enum.GetValues<Key>())
            {
                var state = new KeyState();
                state.Reset();
                foreach (ConsoleKey consoleKey in keys)
                {
                    if (key == Convert(consoleKey))
                    {
                        state.Held = true;
                        break;
                    }
                }
                var lastState = lastValues[key];
                state.Down = !lastState.Held && state.Held;
                state.Up = lastState.Held && !state.Held;
                mKeyStates[key] = state;
            }
        }
        private static Key Convert(ConsoleKey consoleKey)
        {
            foreach (var key in Enum.GetValues<Key>())
            {
                if (key.ToString() == consoleKey.ToString())
                {
                    return key;
                }
            }
            throw new InvalidCastException();
        }
        private Dictionary<Key, KeyState> mKeyStates;
    }
}
