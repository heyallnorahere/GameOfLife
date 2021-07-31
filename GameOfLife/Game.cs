using GameOfLife.Frontends;

namespace GameOfLife
{
    public sealed class Game
    {
        public Game(IFrontend frontend)
        {
            Frontend = frontend;
            Board = new Board(this);
            Running = false;
        }
        public void Run()
        {
            Running = true;
            while (Running)
            {
                var inputManager = Frontend.InputManager;
                inputManager.Update();
                if (inputManager[Key.Q].Down)
                {
                    Quit();
                }
                Board.Update();
                Render();
            }
        }
        private void Render()
        {
            var cells = Board.Cells;
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
        public IFrontend Frontend { get; private set; }
        public Board Board { get; private set; }
    }
}
