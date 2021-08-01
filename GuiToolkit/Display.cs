using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Silk.NET.Input;
using Silk.NET.Maths;
using Silk.NET.Windowing;

namespace GuiToolkit
{
    public sealed class Display
    {
        public Display(string title, int width, int height)
        {
            var options = WindowOptions.DefaultVulkan; // we're gonna use this with vulkan
            options.Size = new Vector2D<int>(width, height);
            options.Title = title;
            mWindow = Window.Create(options);
            mWindow.Load += OnLoad;
            mWindow.Update += OnUpdate;
            mWindow.Render += OnRender;
        }
        public void Run()
        {
            mWindow.Run();
        }
        private void OnLoad()
        {
            InputManager = new InputManager(mWindow.CreateInput());
            Load?.Invoke();
        }
        private void OnUpdate(double obj)
        {
            InputManager?.UpdateAndSwapStates();
            Update?.Invoke(obj);
        }
        private void OnRender(double obj)
        {
            Render?.Invoke(obj);
        }
        private readonly IWindow mWindow;
        public InputManager? InputManager { get; private set; }
        public event Action? Load;
        public event Action<double>? Update;
        public event Action<double>? Render;
    }
}
