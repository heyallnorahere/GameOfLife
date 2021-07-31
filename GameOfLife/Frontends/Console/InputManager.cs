using System;
using System.Collections.Generic;

namespace GameOfLife.Frontends.Console
{
    internal sealed class InputManager : IInputManager
    {
        public InputManager()
        {
            mKeyStates = new();
        }
        public bool this[Key key] => GetKey(key);
        public bool GetKey(Key key)
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
            mKeyStates = new();
            foreach (var key in Enum.GetValues<Key>())
            {
                var state = false;
                foreach (ConsoleKey consoleKey in keys)
                {
                    if (key == Convert(consoleKey))
                    {
                        state = true;
                        break;
                    }
                }
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
        private Dictionary<Key, bool> mKeyStates;
    }
}
