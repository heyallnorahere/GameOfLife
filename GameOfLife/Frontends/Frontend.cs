using System;
using System.Collections.Generic;
using System.Reflection;

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
    public delegate void SetupGameInstanceDelegate(Game instance);
    public abstract class Frontend
    {
        static Frontend()
        {
            RegisteredFrontends = new();
            var currentDomain = AppDomain.CurrentDomain;
            var assemblies = currentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                var types = assembly.GetTypes();
                foreach (var type in types)
                {
                    if (!IsDerived(typeof(Frontend), type))
                    {
                        continue;
                    }
                    var attributes = Attribute.GetCustomAttributes(type, typeof(FrontendAttribute));
                    foreach (var attribute in attributes)
                    {
                        if (attribute is FrontendAttribute frontendAttribute)
                        {
                            RegisterFrontend(frontendAttribute.ID, () =>
                            {
                                ConstructorInfo? constructorInfo = type.GetConstructor(Array.Empty<Type>());
                                if (constructorInfo != null)
                                {
                                    return (Frontend)constructorInfo.Invoke(Array.Empty<object>());
                                }
                                throw new ArgumentException($"Could not find suitable constructor for type: {type.FullName}");
                            });
                        }
                    }
                }
            }
        }
        private static bool IsDerived(Type @base, Type derived)
        {
            Type? currentType = derived;
            while (currentType != null)
            {
                currentType = currentType.BaseType;
                if (currentType == @base)
                {
                    return true;
                }
            }
            return false;
        }
        public event SetupGameInstanceDelegate? SetupGameInstance;
        public abstract void Run();
        protected void CallSetupGameInstance(Game instance)
        {
            SetupGameInstance?.Invoke(instance);
        }
        public static Frontend Create(FrontendID frontendID)
        {
            var creationDelegate = RegisteredFrontends[frontendID];
            return creationDelegate();
        }
        internal static void RegisterFrontend(FrontendID frontendID, CreateFrontendDelegate creationDelegate)
        {
            RegisteredFrontends.Add(frontendID, creationDelegate);
        }
        internal delegate Frontend CreateFrontendDelegate();
        private static Dictionary<FrontendID, CreateFrontendDelegate> RegisteredFrontends { get; set; }
    }
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public sealed class FrontendAttribute : Attribute
    {
        public FrontendAttribute(string id)
        {
            ID = id;
        }
        internal string ID { get; private set; }
    }
}
