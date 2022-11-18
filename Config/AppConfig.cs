using AdminGroupBot.Config.ConfigModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace AdminGroupBot.Config
{
    internal class AppConfig
    {
        public static BotConfigModel Bot = new DeserializerBuilder()
            .WithNamingConvention(UnderscoredNamingConvention.Instance)  
            .Build().Deserialize<BotConfigModel>(File.ReadAllText("bot.yml"));

        public static Dictionary<string, string> environmentVariables = File.ReadAllText("bot.yml")
            .Split("\n")
            .ToDictionary<string, string>(str => str);
    }
}
