using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;

namespace GameOfLife
{
    public delegate List<Rule> GetRuleDelegate();
    public static class Config
    {
        static Config()
        {
            InitialState = new List<Vector>();
            AdditionalRules = new List<Rule>();
        }
        public static void Load(string file = "config.json")
        {
            if (!File.Exists(file))
            {
                return;
            }
            using TextReader textReader = new StreamReader(file);
            using JsonReader jsonReader = new JsonTextReader(textReader);
            var serializer = new JsonSerializer();
            var configData = serializer.Deserialize<ConfigData>(jsonReader);
            if (configData.InitialState != null)
            {
                InitialState = configData.InitialState;
            }
            if (configData.RulesetAssemblies != null)
            {
                AdditionalRules.Clear();
                foreach (string assemblyPath in configData.RulesetAssemblies)
                {
                    LoadAdditionalRules(assemblyPath);
                }
            }
        }
        private static void LoadAdditionalRules(string rulesetAssemblyPath)
        {
            var assembly = Assembly.Load(rulesetAssemblyPath);
            if (assembly != null)
            {
                foreach (Type type in assembly.GetTypes())
                {
                    CustomRuleAttribute? attribute = type.GetCustomAttribute<CustomRuleAttribute>();
                    if (attribute != null)
                    {
                        MethodInfo? methodInfo = type.GetMethod(attribute.MethodName, BindingFlags.Static | BindingFlags.Public, null, new Type[] { typeof(BoardController), typeof(Vector) }, null);
                        if (methodInfo == null)
                        {
                            throw new ArgumentException("The specified method does not exist!");
                        }
                        else
                        {
                            AdditionalRules.Add((Rule)Rule.CreateDelegate(type, methodInfo));
                        }
                    }
                }
            }
        }
        public static List<Vector> InitialState { get; private set; }
        public static List<Rule> AdditionalRules { get; private set; }
        [JsonObject]
        private struct ConfigData
        {
            [JsonProperty(NullValueHandling = NullValueHandling.Include)]
            public List<Vector>? InitialState { get; set; }
            [JsonProperty(NullValueHandling = NullValueHandling.Include)]
            public List<string>? RulesetAssemblies { get; set; }
        }
    }
}
