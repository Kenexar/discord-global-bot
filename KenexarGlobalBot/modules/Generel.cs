using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace KenexarGlobalBot.modules;

public class Generel : ModuleBase
{
    [Command("ping")]
    public async Task Ping()
    {
        await Context.Channel.SendMessageAsync("Pong!");
    }

    [Command("info")]
    public async Task Info(params String[] args)
    {
        Console.WriteLine(Context.Message);
        Console.WriteLine(String.Join(" ", args));
        var builder = new EmbedBuilder()
            .WithThumbnailUrl(Context.User.GetAvatarUrl() ?? Context.User.GetDefaultAvatarUrl())
            .WithDescription($"here you have the Avatar url from {Context.User.Username}")
            .WithColor(new Color(164, 164, 164))
            .AddField("UserID", Context.User.Id, true)
            .AddField("Discriminator", Context.User.Discriminator, true)
            .AddField("Created at", Context.User.CreatedAt.ToString("dd.MM.yy"), true)
            .AddField("Joined at", (Context.User as SocketGuildUser).JoinedAt.Value.ToString("dd.MM.yy"), true)
            .AddField("Roles", string.Join("\n", (Context.User as SocketGuildUser).Roles), true)
            .WithCurrentTimestamp();
        
        var embed = builder.Build();
        await Context.Channel.SendMessageAsync(null, false, embed);
    }
}