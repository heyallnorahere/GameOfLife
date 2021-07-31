using System;
using GameOfLife.Frontends.Console;

namespace GameOfLife.Frontends
{
    public enum FrontendType
    {
        Console
    }
    public abstract class Frontend
    {
        public abstract IRenderer Renderer { get; }
        public abstract IInputManager InputManager { get; }
        public static Frontend Create(FrontendType frontendType)
        {
            return frontendType switch
            {
                FrontendType.Console => new ConsoleFrontend(),
                _ => throw new InvalidOperationException()
            };
        }
    }
}
