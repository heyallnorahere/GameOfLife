using System;
using GuiToolkit;

namespace GameOfLife.Frontends.Gui
{
    [Frontend("GUI")]
    internal sealed class GuiFrontend : Frontend
    {
        public GuiFrontend() { }
        public override void Run()
        {
            mDisplay = new Display("Game of Life", 800, 600);
            mDisplay.Load += OnLoad;
            mDisplay.Update += OnUpdate;
            mDisplay.Render += OnRender;
            mDisplay.Run();
        }
        private void OnLoad()
        {
            var guiToolkitInputManager = mDisplay?.InputManager;
            mInputManager = new InputManager(guiToolkitInputManager ?? throw new NullReferenceException());
            mInstance = new Game();
            CallSetupGameInstance(mInstance);
            mInstance.Prepare();
        }
        private void OnUpdate(double obj)
        {
            mInstance?.Update(mInputManager ?? throw new NullReferenceException());
        }
        private void OnRender(double obj)
        {
            mInstance?.Render(mRenderer ?? throw new NullReferenceException());
        }
        private IInputManager? mInputManager;
        private IRenderer? mRenderer;
        private Display? mDisplay;
        private Game? mInstance;
    }
}
