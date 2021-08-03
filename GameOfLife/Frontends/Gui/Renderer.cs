using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GuiToolkit;

namespace GameOfLife.Frontends.Gui
{
    internal class Renderer : IRenderer
    {
        public Renderer(GuiToolkit.Renderer renderer)
        {
            mRenderer = renderer;
            mRenderScope = null;
        }
        public RenderScope RenderScope { set => mRenderScope = value; }
        public void BeginRender()
        {
            mRenderer.ClearScreen();
        }
        public void EndRender()
        {
            // im not sure EndRender() serves any purpose in this case, though i may be wrong
        }
        public void RenderCell(Vector position)
        {
            throw new NotImplementedException();
        }
        private RenderScope? mRenderScope;
        private readonly GuiToolkit.Renderer mRenderer;
    }
}
