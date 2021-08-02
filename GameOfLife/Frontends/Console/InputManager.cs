using System;
using System.Collections.Generic;

namespace GameOfLife.Frontends.Console
{
    internal sealed class InputManager : IInputManager
    {
        static InputManager()
        {
            TranslationTable = new Dictionary<ConsoleKey, InputAction>
            {
                [ConsoleKey.Q] = InputAction.Quit,
                [ConsoleKey.W] = InputAction.PanUp,
                [ConsoleKey.S] = InputAction.PanDown,
                [ConsoleKey.A] = InputAction.PanLeft,
                [ConsoleKey.D] = InputAction.PanRight,
                [ConsoleKey.OemMinus] = InputAction.ZoomOut,
                [ConsoleKey.OemPlus] = InputAction.ZoomIn,
                [ConsoleKey.UpArrow] = InputAction.FPSUp,
                [ConsoleKey.DownArrow] = InputAction.FPSDown,
                [ConsoleKey.LeftArrow] = InputAction.PreviousFrame,
                [ConsoleKey.RightArrow] = InputAction.NextFrame,
                [ConsoleKey.Spacebar] = InputAction.Pause
            };
        }
        private static Dictionary<ConsoleKey, InputAction> TranslationTable { get; set; }
        public InputManager()
        {
            mKeyStates = new();
        }
        public bool this[InputAction action] => IsInputHeld(action);
        public bool IsInputHeld(InputAction action)
        {
            return mKeyStates[action];
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
            foreach (var action in Enum.GetValues<InputAction>())
            {
                var state = false;
                foreach (ConsoleKey consoleKey in keys)
                {
                    if (TranslationTable.ContainsKey(consoleKey))
                    {
                        if (action == TranslationTable[consoleKey])
                        {
                            state = true;
                            break;
                        }
                    }
                }
                mKeyStates[action] = state;
            }
        }
        private Dictionary<InputAction, bool> mKeyStates;
    }
}
