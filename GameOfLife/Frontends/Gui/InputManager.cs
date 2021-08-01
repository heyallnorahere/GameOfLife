using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GuiToolkit;

namespace GameOfLife.Frontends.Gui
{
    internal sealed class InputManager : IInputManager
    {
        public InputManager(GuiToolkit.InputManager inputManager)
        {
            mKeyStates = new();
            mInputManager = inputManager;
        }
        public bool this[Key key] => GetKey(key);
        public bool GetKey(Key key)
        {
            return mKeyStates[key];
        }
        public void Update()
        {
            foreach (var key in Enum.GetValues<Key>())
            {
                foreach (var silkKey in Enum.GetValues<Silk.NET.Input.Key>())
                {
                    if (key.ToString() == silkKey.ToString())
                    {
                        mKeyStates[key] = mInputManager[silkKey].Held;
                    }
                }
            }
        }
        private readonly GuiToolkit.InputManager mInputManager;
        private readonly Dictionary<Key, bool> mKeyStates;
    }
}
