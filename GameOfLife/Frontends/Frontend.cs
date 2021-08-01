using System;
using System.Collections.Generic;
using GameOfLife.Frontends.Console;

namespace GameOfLife.Frontends
{
    public struct FrontendID
    {
        public FrontendID(string id)
        {
            ID = id;
        }
        internal string ID { get; private set; }
        public static implicit operator FrontendID(string id) => new(id);
        public static bool operator==(FrontendID id1, FrontendID id2)
        {
            return id1 == id2.ID;
        }
        public static bool operator!=(FrontendID id1, FrontendID id2)
        {
            return !(id1 == id2);
        }
        public static bool operator==(FrontendID id1, string id2)
        {
            return id1.ID.ToLower() == id2.ToLower();
        }
        public static bool operator!=(FrontendID id1, string id2)
        {
            return !(id1 == id2);
        }
        public override bool Equals(object? obj)
        {
            if (obj is FrontendID frontendID)
            {
                return this == frontendID;
            }
            else if (obj is string id)
            {
                return this == id;
            }
            else
            {
                return false;
            }
        }
        public override int GetHashCode()
        {
            return ID.ToLower().GetHashCode();
        }
    }
    public abstract class Frontend
    {
        static Frontend()
        {
            RegisteredFrontends = new();
            ConsoleFrontend.Register();
        }
        public abstract IRenderer Renderer { get; }
        public abstract IInputManager InputManager { get; }
        public static Frontend Create(FrontendID frontendID)
        {
            var creationDelegate = RegisteredFrontends[frontendID];
            return creationDelegate();
        }
        protected static void RegisterFrontend(FrontendID frontendID, CreateFrontendDelegate creationDelegate)
        {
            RegisteredFrontends.Add(frontendID, creationDelegate);
        }
        protected delegate Frontend CreateFrontendDelegate();
        private static Dictionary<FrontendID, CreateFrontendDelegate> RegisteredFrontends { get; set; }
    }
}
