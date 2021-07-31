using System.Collections.Generic;
using GameOfLife.Frontends;

namespace GameOfLife
{
    public sealed class Game
    {
        public Game(Frontend frontend)
        {
            Frontend = frontend;
            Board = new Board(this);
            FrameManager = new FrameManager();
            Frontend.Renderer.RenderScope = FrameManager.RenderScope;
            Running = false;
        }
        public void Run()
        {
            // add the initial state
            FrameManager.AddFrame(Board.Cells);
            Running = true;
            while (Running)
            {
                Update();
                Render();
            }
        }
        private void Update()
        {
            FrameManager.NewFrame();
            var inputManager = Frontend.InputManager;
            inputManager.Update();
            if (inputManager[Key.Q].Down)
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
        private void Render()
        {
            var cells = FrameManager.GetCurrentFrame();
            var renderer = Frontend.Renderer;
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
        public Frontend Frontend { get; private set; }
        public Board Board { get; private set; }
        internal FrameManager FrameManager { get; private set; }
    }
}
