using System.Collections.Generic;
using Silk.NET.Input;

namespace GameOfLife.Frontends.Gui
{
    internal sealed class InputManager : IInputManager
    {
        static InputManager()
        {
            TranslationTable = new()
            {
                [Key.Q] = InputAction.Quit,
                [Key.W] = InputAction.PanUp,
                [Key.S] = InputAction.PanDown,
                [Key.A] = InputAction.PanLeft,
                [Key.D] = InputAction.PanRight,
                [Key.Minus] = InputAction.ZoomOut,
                [Key.Equal] = InputAction.ZoomIn,
                [Key.Up] = InputAction.FPSUp,
                [Key.Down] = InputAction.FPSDown,
                [Key.Left] = InputAction.PreviousFrame,
                [Key.Right] = InputAction.NextFrame,
                [Key.Space] = InputAction.Pause
            };
        }
        private static Dictionary<Key, InputAction> TranslationTable { get; set; }
        public InputManager(GuiToolkit.InputManager inputManager)
        {
            mInputStates = new();
            mInputManager = inputManager;
        }
        public bool this[InputAction action] => IsInputHeld(action);
        public bool IsInputHeld(InputAction action)
        {
            return mInputStates[action];
        }
        public void Update()
        {
            foreach (var pair in TranslationTable)
            {
                mInputStates[pair.Value] = mInputManager[pair.Key].Held;
            }
        }
        private readonly GuiToolkit.InputManager mInputManager;
        private readonly Dictionary<InputAction, bool> mInputStates;
    }
}
