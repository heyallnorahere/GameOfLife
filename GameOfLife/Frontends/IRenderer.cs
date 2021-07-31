namespace GameOfLife.Frontends
{
    public interface IRenderer
    {
        void BeginRender();
        void EndRender();
        void RenderCell(Vector position);
        RenderScope RenderScope { set; }
    }
}
