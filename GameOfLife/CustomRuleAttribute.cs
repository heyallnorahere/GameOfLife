using System;

namespace GameOfLife
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
    public class CustomRuleAttribute : Attribute
    {
        public CustomRuleAttribute(string methodName)
        {
            MethodName = methodName;
        }
        internal string MethodName { get; }
    }
}