using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace GameOfLife.Frontends.Console
{
    internal sealed class Renderer : IRenderer
    {
        public Renderer()
        {
            mRenderScope = null;
            mCells = new HashSet<Vector>();
        }
        public void BeginRender()
        {
            mCells.Clear();
        }
        public void EndRender()
        {
            System.Console.CursorVisible = false;
            if (mRenderScope == null)
            {
                return;
            }
            Vector scopeSize = mRenderScope.Size;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                // for some reason this call is exclusive to windows
                System.Console.SetWindowSize(scopeSize.X * 2, scopeSize.Y + 1);
            }
            int bufferWidth = scopeSize.X + 1;
            var buffer = new char[bufferWidth * scopeSize.Y];
            for (int y = 0; y < scopeSize.Y; y++)
            {
                for (int x = 0; x < scopeSize.X; x++)
                {
                    int bufferIndex = (y * bufferWidth) + x;
                    buffer[bufferIndex] = ' ';
                }
                if (y > 0)
                {
                    buffer[(y * bufferWidth) - 1] = '\n';
                }
            }
            foreach (Vector cell in mCells)
            {
                Vector renderPosition = cell + (scopeSize / 2);
                int bufferIndex = (renderPosition.Y * bufferWidth) + renderPosition.X;
                buffer[bufferIndex] = '\u2588';
            }
            System.Console.SetCursorPosition(0, 0);
            System.Console.Write(buffer);
        }
        public void RenderCell(Vector position)
        {
            if (mRenderScope == null)
            {
                return;
            }
            Vector convertedPosition = position - mRenderScope.Center;
            if (Math.Abs(convertedPosition.X) >= mRenderScope.Size.X / 2)
            {
                return;
            }
            if (Math.Abs(convertedPosition.Y) >= mRenderScope.Size.Y / 2)
            {
                return;
            }
            mCells.Add(convertedPosition);
        }
        public RenderScope RenderScope
        {
            set => mRenderScope = value;
        }
        private readonly HashSet<Vector> mCells;
        private RenderScope? mRenderScope;
    }
}
