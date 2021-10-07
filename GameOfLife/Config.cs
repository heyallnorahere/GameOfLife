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
            string? dirName = Path.GetDirectoryName(file);
            if (configData.RulesetAssemblies != null)
            {
                AdditionalRules.Clear();
                foreach (string assemblyPath in configData.RulesetAssemblies)
                {
                    LoadAdditionalRules(Path.Join(dirName, assemblyPath));
                }
            }
        }
        private static void LoadAdditionalRules(string rulesetAssemblyPath)
        {
            var assembly = Assembly.LoadFrom(rulesetAssemblyPath);
            RemoveDefaultRules = false;
            if (assembly != null)
            {
                var ruleAssemblyAttribute = assembly.GetCustomAttribute<RuleAssemblyAttribute>();
                if (ruleAssemblyAttribute != null)
                {
                    if (!RemoveDefaultRules)
                    {
                        RemoveDefaultRules = ruleAssemblyAttribute.RemoveDefaultRules;
                    }
                }
                var rules = CustomRuleAttribute.GetRulesFromAssembly(assembly);
                AdditionalRules.AddRange(rules);
            }
        }
        public static List<Vector> InitialState { get; private set; }
        public static List<Rule> AdditionalRules { get; private set; }
        public static bool RemoveDefaultRules { get; private set; } = false;
        [JsonObject]
        public struct ConfigData
        {
            [JsonProperty(NullValueHandling = NullValueHandling.Include)]
            public List<Vector>? InitialState { get; set; }
            [JsonProperty(NullValueHandling = NullValueHandling.Include)]
            public List<string>? RulesetAssemblies { get; set; }
        }
    }
}
