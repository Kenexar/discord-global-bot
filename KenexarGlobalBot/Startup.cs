using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;
using KenexarGlobalBot.services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KenexarGlobalBot;

public class Startup
{
    public IConfigurationRoot Configuration { get; }

    public Startup(string[] args) // This is part creates an YAML File for configurations
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddYamlFile("_config.yml");
        
        Configuration = builder.Build();
    }

    public static async Task RunAsync(string[] args)
    {
        var startup = new Startup(args);
        await startup.RunAsync();
    }

    public async Task RunAsync()
    {
        var services = new ServiceCollection();
        ConfigureServices(services);

        var provider = services.BuildServiceProvider();
        provider.GetRequiredService<CommandHandler>();

        await provider.GetRequiredService<StartupService>().StartAsync();
        await Task.Delay(-1);
    }

    private void ConfigureServices(ServiceCollection services)
    {
        services.AddSingleton(new DiscordSocketClient(new DiscordSocketConfig
        {
            LogLevel = Discord.LogSeverity.Debug,
            MessageCacheSize = 1000
        }))
        .AddSingleton(new CommandService(new CommandServiceConfig
        {
            LogLevel = Discord.LogSeverity.Debug,
            DefaultRunMode = RunMode.Async,
            CaseSensitiveCommands = false
        }))
        .AddSingleton<CommandHandler>()
        .AddSingleton<StartupService>()
        .AddSingleton(Configuration);
    }
}