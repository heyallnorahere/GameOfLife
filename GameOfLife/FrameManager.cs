using System;
using System.Collections.Generic;
using GameOfLife.Frontends;

namespace GameOfLife
{
    public sealed class RenderScope
    {
        internal RenderScope()
        {
            mCenter = (0, 0);
            mSize = (100, 30);
        }
        public void Update(IInputManager inputManager)
        {
            if (inputManager[Key.W].Down)
            {
                mCenter.Y++;
            }
            if (inputManager[Key.S].Down)
            {
                mCenter.Y--;
            }
            if (inputManager[Key.A].Down)
            {
                mCenter.X--;
            }
            if (inputManager[Key.D].Down)
            {
                mCenter.X++;
            }
            if (inputManager[Key.OemMinus].Down)
            {
                mSize += 2;
            }
            if (inputManager[Key.OemPlus].Down)
            {
                mSize -= 2;
            }
        }
        public Vector Center => mCenter;
        public Vector Size => mSize;
        private Vector mCenter, mSize;
    }
    internal sealed class FrameManager
    {
        public FrameManager()
        {
            FPS = 1;
            CurrentFrameIndex = 0;
            Paused = false;
            CanUpdate = false;
            mFrames = new();
            mRenderScope = new RenderScope();
            mLastUpdate = null;
        }
        public void NewFrame()
        {
            var now = DateTime.Now;
            if (mLastUpdate == null)
            {
                mLastUpdate = now;
            }
            var frameTime = (now - (mLastUpdate ?? throw new NullReferenceException())).Ticks;
            long frameDelay = TimeSpan.TicksPerSecond / FPS;
            if (frameTime >= frameDelay)
            {
                CanUpdate = true;
                mLastUpdate = now;
            }
            else
            {
                CanUpdate = false;
            }
        }
        public void AddFrame(HashSet<Vector> boardState)
        {
            mFrames.Add(boardState);
        }
        public void Update(IInputManager inputManager, HashSet<Vector>? boardState)
        {
            if (boardState != null)
            {
                AddFrame(boardState);
            }
            if (inputManager[Key.UpArrow].Down)
            {
                FPS++;
            }
            if (inputManager[Key.DownArrow].Down && FPS > 1)
            {
                FPS--;
            }
            if (inputManager[Key.Spacebar].Down)
            {
                Paused = !Paused;
                if (!Paused)
                {
                    CurrentFrameIndex = 0;
                }
            }
            if (inputManager[Key.LeftArrow].Down)
            {
                if (!Paused)
                {
                    Paused = true;
                }
                if (CurrentFrameIndex < mFrames.Count - 1)
                {
                    CurrentFrameIndex++;
                }
            }
            if (inputManager[Key.RightArrow].Down)
            {
                if (!Paused)
                {
                    Paused = true;
                }
                if (CurrentFrameIndex > 0)
                {
                    CurrentFrameIndex--;
                }
            }
            RenderScope.Update(inputManager);
        }
        public HashSet<Vector> GetCurrentFrame()
        {
            return mFrames[^(CurrentFrameIndex + 1)];
        }
        public int FPS { get; set; }
        public int CurrentFrameIndex { get; set; }
        public bool Paused { get; set; }
        public bool CanUpdate { get; private set; }
        public RenderScope RenderScope => mRenderScope;
        private readonly List<HashSet<Vector>> mFrames;
        private readonly RenderScope mRenderScope;
        private DateTime? mLastUpdate;
    }
}
