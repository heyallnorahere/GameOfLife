using System.Collections.Generic;
using GameOfLife.Frontends;

namespace GameOfLife
{
    internal class FrameManager
    {
        public FrameManager()
        {
            FPS = 1;
            CurrentFrameIndex = 0;
            Paused = false;
            mFrames = new();
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
        }
        public HashSet<Vector> GetCurrentFrame()
        {
            return mFrames[^(CurrentFrameIndex + 1)];
        }
        public int FPS { get; set; }
        public int CurrentFrameIndex { get; set; }
        public bool Paused { get; set; }
        private readonly List<HashSet<Vector>> mFrames;
    }
}
