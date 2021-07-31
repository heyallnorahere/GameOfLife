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
                foreach (RulesetAssembly assembly in configData.RulesetAssemblies)
                {
                    LoadAdditionalRules(assembly);
                }
            }
        }
        private static void LoadAdditionalRules(RulesetAssembly rulesetAssembly)
        {
            if (!File.Exists(rulesetAssembly.AssemblyPath))
            {
                return;
            }
            var assembly = Assembly.Load(rulesetAssembly.AssemblyPath);
            var entrypoint = assembly.GetType(rulesetAssembly.Entrypoint);
            MethodInfo? method = entrypoint?.GetMethod("GetDelegate", BindingFlags.Static | BindingFlags.Public);
            GetRuleDelegate? @delegate = (GetRuleDelegate?)method?.Invoke(null, null);
            List<Rule>? rules = @delegate?.Invoke();
            if (rules != null)
            {
                AdditionalRules.AddRange(rules);
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
            public List<RulesetAssembly>? RulesetAssemblies { get; set; }
        }
        [JsonObject]
        private struct RulesetAssembly
        {
            public string AssemblyPath { get; set; }
            public string Entrypoint { get; set; }
        }
    }
}
