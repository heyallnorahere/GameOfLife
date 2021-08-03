using System.Collections.Generic;
using Silk.NET.OpenGL;

namespace GuiToolkit.OpenGL
{
    internal class ElementBufferObject : IBuffer
    {
        public ElementBufferObject(GL glInterface)
        {
            mGLInterface = glInterface;
            mID = mGLInterface.CreateBuffer();
            mIndexCount = 0;
        }
        public unsafe void SetData(List<uint> indices)
        {
            mIndexCount = indices.Count;
            Bind();
            var indexArray = new uint[indices.Count];
            indices.CopyTo(indexArray);
            fixed (void* indicesPointer = &indexArray[0])
            {
                mGLInterface.BufferData(BufferTargetARB.ElementArrayBuffer, (nuint)(indices.Count * sizeof(uint)), indicesPointer, BufferUsageARB.StaticDraw);
            }
        }
        public object ID => mID;
        public int ObjectCount => mIndexCount;
        public void Bind()
        {
            mGLInterface.BindBuffer(BufferTargetARB.ElementArrayBuffer, mID);
        }
        public void Unbind()
        {
            mGLInterface.BindBuffer(BufferTargetARB.ElementArrayBuffer, 0);
        }
        public void Destroy()
        {
            mGLInterface.DeleteBuffer(mID);
        }
        private readonly GL mGLInterface;
        private readonly uint mID;
        private int mIndexCount;
    }
}
