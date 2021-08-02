using System;
using System.Collections.Generic;
using GuiToolkit.OpenGL;

namespace GuiToolkit
{
    public enum RendererAPI
    {
        OpenGL
    }
    public struct RenderedObject<VertexType> where VertexType : unmanaged
    {
        public List<VertexType> Vertices { get; set; }
        public List<uint>? Indices { get; set; }
    }
    public interface IAssembledObject
    {
        object VAO { get; }
        object? EBO { get; }
    }
    public sealed class CommandList
    {
        public CommandList()
        {
            mObjects = new List<IAssembledObject>();
            Open = true;
        }
        public void Submit(IAssembledObject assembledObject)
        {
            if (!Open)
            {
                throw new InvalidOperationException("The CommandList was already closed!");
            }
            mObjects.Add(assembledObject);
        }
        public void Execute(Renderer renderer)
        {
            if (!Open)
            {
                throw new InvalidOperationException("The CommandList was already executed!");
            }
            Open = false;
            renderer.ExecuteCommandList(this);
        }
        public bool Open { get; private set; }
        internal List<IAssembledObject> Objects => new(mObjects);
        private readonly List<IAssembledObject> mObjects;
    }
    public abstract class Renderer
    {
        public static Renderer Create(Display display)
        {
            return display.mAPI switch
            {
                RendererAPI.OpenGL => new OpenGLRenderer(display),
                _ => throw new ArgumentException("Invalid renderer API!")
            };
        }
        internal void ExecuteCommandList(CommandList commandList)
        {
            var assembledObjects = commandList.Objects;
            RenderObjects(assembledObjects);
        }
        public abstract IAssembledObject CreateBuffers<VertexType>(RenderedObject<VertexType> renderedObject) where VertexType : unmanaged;
        public abstract void DestroyBuffers(IAssembledObject assembledObject);
        protected abstract void RenderObjects(List<IAssembledObject> assembledObjects);
    }
}
