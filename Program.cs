using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DSharpPlus;

namespace Wololo.Discord.Bot
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            
            var serviceProvider = serviceCollection.BuildServiceProvider();
            await serviceProvider.GetService<App>().Run();
        }

        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false)
                .AddUserSecrets<Program>()
                .AddEnvironmentVariables()
                .Build();

            serviceCollection.AddSingleton<IConfiguration>(configuration);
            
            var discordClient = new DiscordClient(new DiscordConfiguration
            {
                Token = configuration["DISCORD_BOT_TOKEN"],
                TokenType = TokenType.Bot
            });

            serviceCollection.AddSingleton<DiscordClient>(discordClient);

            serviceCollection.AddTransient<App>();
        }
    }
}
