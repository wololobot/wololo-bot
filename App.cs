using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using DSharpPlus;

namespace Wololo.Discord.Bot
{
    public class App 
    {
        private IConfiguration _configuration;
        private DiscordClient _discordClient;

        public App(IConfiguration configuration, DiscordClient discordClient)
        {
            this._configuration = configuration;
            this._discordClient = discordClient;
        }

        public async Task Run()
        {
            _discordClient.MessageCreated += async e =>
            {
                if (e.Message.Content.ToLower().StartsWith("ping"))
                    await e.Message.RespondAsync("pong!");
            };

            await _discordClient.ConnectAsync();
            await Task.Delay(-1);
        }
    }
}