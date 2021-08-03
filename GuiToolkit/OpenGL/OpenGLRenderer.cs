using System;
using System.Collections.Generic;
using Silk.NET.OpenGL;

namespace GuiToolkit.OpenGL
{
    internal sealed class OpenGLRenderer : Renderer
    {
        private struct AssembledObject : IAssembledObject
        {
            public IBuffer VBO => RealVBO;
            public IBuffer? EBO => RealEBO;
            public VertexBufferObject RealVBO { get; set; }
            public ElementBufferObject? RealEBO { get; set; }
        }
        public OpenGLRenderer(Display display)
        {
            mDisplay = display;
            mGLInterface = GL.GetApi(mDisplay.NativeWindow);
            mGLInterface.Enable(EnableCap.DepthTest);
        }
        public override IAssembledObject CreateBuffers<VertexType>(RenderedObject<VertexType> renderedObject)
        {
            var assembledObject = new AssembledObject
            {
                RealVBO = new VertexBufferObject(mGLInterface)
            };
            assembledObject.RealVBO.SetData(renderedObject.Vertices);
            if (renderedObject.Indices != null)
            {
                assembledObject.RealEBO = new ElementBufferObject(mGLInterface);
                assembledObject.RealEBO.SetData(renderedObject.Indices);
            }
            return assembledObject;
        }
        public override void DestroyBuffers(IAssembledObject assembledObject)
        {
            if (assembledObject is AssembledObject specificTypedObject)
            {
                specificTypedObject.RealVBO.Destroy();
                specificTypedObject.RealEBO?.Destroy();
            }
            else
            {
                throw new ArgumentException("The provided AssembledObject does not contain OpenGL buffers!");
            }
        }
        public override void ClearScreen()
        {
            mGLInterface.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        }
        protected override void RenderObjects(List<IAssembledObject> assembledObjects)
        {
            throw new NotImplementedException();
        }
        private readonly Display mDisplay;
        private readonly GL mGLInterface;
    }
}
