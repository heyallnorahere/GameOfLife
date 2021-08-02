using System.Collections.Generic;
using GameOfLife.Frontends;

namespace GameOfLife
{
    public sealed class Game
    {
        public Game()
        {
            Board = new Board(this);
            FrameManager = new FrameManager();
            Running = false;
        }
        public void Run(IInputManager inputManager, IRenderer renderer)
        {
            Prepare();
            while (Running)
            {
                Update(inputManager);
                Render(renderer);
            }
        }
        public void Prepare()
        {
            // add the initial state
            FrameManager.AddFrame(Board.Cells);
            Running = true;
        }
        public void Update(IInputManager inputManager)
        {
            FrameManager.NewFrame();
            inputManager.Update();
            if (inputManager[InputAction.Quit])
            {
                Quit();
            }
            HashSet<Vector>? boardState = null;
            if (FrameManager.CanUpdate && !FrameManager.Paused)
            {
                Board.Update();
                boardState = Board.Cells;
            }
            FrameManager.Update(inputManager, boardState);
        }
        public void Render(IRenderer renderer)
        {
            var cells = FrameManager.GetCurrentFrame();
            renderer.BeginRender();
            foreach (Vector cell in cells)
            {
                renderer.RenderCell(cell);
            }
            renderer.EndRender();
        }
        public void Quit()
        {
            Running = false;
        }
        public bool Running { get; private set; }
        public Board Board { get; private set; }
        internal FrameManager FrameManager { get; private set; }
    }
}
