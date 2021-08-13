using System;
using System.Collections.Generic;
using System.Reflection;

namespace GameOfLife
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class CustomRuleAttribute : Attribute
    {
        public CustomRuleAttribute() { }
        internal static List<Rule> GetRulesFromAssembly(Assembly assembly)
        {
            var rules = new List<Rule>();
            foreach (Type type in assembly.GetTypes())
            {
                MethodInfo[] methods = type.GetMethods(BindingFlags.Static | BindingFlags.Public);
                foreach (MethodInfo methodInfo in methods)
                {
                    IEnumerable<CustomRuleAttribute> attributes = methodInfo.GetCustomAttributes<CustomRuleAttribute>();
                    foreach (CustomRuleAttribute attribute in attributes)
                    {
                        rules.Add((Rule)Delegate.CreateDelegate(typeof(Rule), methodInfo));
                    }
                }
            }
            return rules;
        }
    }
}