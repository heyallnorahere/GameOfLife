using System.Collections.Generic;
using Silk.NET.OpenGL;

namespace GuiToolkit.OpenGL
{
    internal class VertexBufferObject : IBuffer
    {
        public VertexBufferObject(GL glInterface)
        {
            mGLInterface = glInterface;
            mID = mGLInterface.CreateBuffer();
            mVertexCount = 0;
        }
        public unsafe void SetData<VertexType>(List<VertexType> vertices) where VertexType : unmanaged
        {
            mVertexCount = vertices.Count;
            Bind();
            var vertexArray = new VertexType[vertices.Count];
            vertices.CopyTo(vertexArray);
            fixed (void* verticesPointer = &vertexArray[0])
            {
                mGLInterface.BufferData(BufferTargetARB.ArrayBuffer, (nuint)(vertices.Count * sizeof(VertexType)), verticesPointer, BufferUsageARB.StaticDraw);
            }
        }
        public void Destroy()
        {
            mGLInterface.DeleteBuffer(mID);
        }
        public void Bind()
        {
            mGLInterface.BindBuffer(BufferTargetARB.ArrayBuffer, mID);
        }
        public void Unbind()
        {
            mGLInterface.BindBuffer(BufferTargetARB.ArrayBuffer, 0);
        }
        public object ID => mID;
        public int ObjectCount => mVertexCount;
        private readonly GL mGLInterface;
        private readonly uint mID;
        private int mVertexCount;
    }
}
