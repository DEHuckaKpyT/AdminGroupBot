using AdminGroupBot.Configuration.ConfigModel;
using System.Text;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using dotenv.net;
using System.Text.RegularExpressions;

namespace AdminGroupBot.Configuration
{
    internal class Config
    {
        public static BotConfigModel Bot { get; }

        public static IDictionary<string, string> EnvironmentVariables { get; }

        static Config()
        {
            Bot = new DeserializerBuilder()
                .WithNamingConvention(UnderscoredNamingConvention.Instance)
                .Build()
                .Deserialize<BotConfigModel>(File.ReadAllText("bot.yml", Encoding.UTF8));

            DotEnv.Load(options: new DotEnvOptions(encoding: Encoding.UTF8));
            EnvironmentVariables = DotEnv.Read();
        }
    }
}
