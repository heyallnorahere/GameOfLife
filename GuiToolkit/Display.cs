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
        public Display(string title, int width, int height, RendererAPI api = RendererAPI.OpenGL)
        {
            mAPI = api;
            var options = mAPI switch
            {
                RendererAPI.OpenGL => WindowOptions.Default,
                _ => throw new ArgumentException("The specified renderer API does not exist!")
            };
            options.Size = new Vector2D<int>(width, height);
            options.Title = title;
            options.VSync = false;
            options.ShouldSwapAutomatically = true;
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
        internal readonly IWindow mWindow;
        internal readonly RendererAPI mAPI;
        public InputManager? InputManager { get; private set; }
        public event Action? Load;
        public event Action<double>? Update;
        public event Action<double>? Render;
    }
}
