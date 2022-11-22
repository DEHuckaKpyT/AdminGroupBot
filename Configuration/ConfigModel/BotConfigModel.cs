namespace AdminGroupBot.Configuration.ConfigModel
{
    internal class BotConfigModel
    {
        public List<string> RemoveMessagePatterns { get; set; }

        public List<long> WhiteListIds { get; set; }

        public List<long> BlackListIds { get; set; }
    }
}
