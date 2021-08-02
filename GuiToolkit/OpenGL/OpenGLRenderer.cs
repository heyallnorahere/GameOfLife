using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Silk.NET.OpenGL;

namespace GuiToolkit.OpenGL
{
    internal sealed class OpenGLRenderer : Renderer
    {
        private struct AssembledObject : IAssembledObject
        {
            public object VAO { get; set; }
            public object? EBO { get; set; }
        }
        public OpenGLRenderer(Display display)
        {
            mDisplay = display;
            mGLInterface = GL.GetApi(mDisplay.mWindow);
        }
        public override IAssembledObject CreateBuffers<VertexType>(RenderedObject<VertexType> renderedObject)
        {
            throw new NotImplementedException();
        }
        public override void DestroyBuffers(IAssembledObject assembledObject)
        {
            throw new NotImplementedException();
        }
        protected override void RenderObjects(List<IAssembledObject> assembledObjects)
        {
            throw new NotImplementedException();
        }
        private readonly Display mDisplay;
        private readonly GL mGLInterface;
    }
}
