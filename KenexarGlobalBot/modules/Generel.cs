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
    public async Task Info(SocketGuildUser user = null)
    {
        if (user == null)
        {
            var builder = new EmbedBuilder()
                .WithThumbnailUrl(Context.User.GetAvatarUrl() ?? Context.User.GetDefaultAvatarUrl())
                .WithDescription($"here you have the Avatar url from {Context.User.Username}")
                .WithColor(new Color(164, 164, 164))
                .AddField("UserID", Context.User.Id, true)
                .AddField("Created at", Context.User.CreatedAt.ToString("dd.MM.yy"), true)
                .AddField("Joined at", (Context.User as SocketGuildUser).JoinedAt.Value.ToString("dd.MM.yy"), true)
                .AddField("Roles", string.Join("\n", (Context.User as SocketGuildUser).Roles.Select(x => x.Mention)), true)
                .WithCurrentTimestamp();
            
            var embed = builder.Build();
            await Context.Channel.SendMessageAsync(null, false, embed);
        }
        else
        {
            var builder = new EmbedBuilder()
                .WithThumbnailUrl(user.GetAvatarUrl() ?? user.GetDefaultAvatarUrl())
                .WithDescription($"here you have the Avatar url from {user.Username}")
                .WithColor(new Color(164, 164, 164))
                .AddField("UserID", user.Id, true)
                .AddField("Created at", user.CreatedAt.ToString("dd.MM.yy"), true)
                .AddField("Joined at", (user as SocketGuildUser).JoinedAt.Value.ToString("dd.MM.yy"), true)
                .AddField("Roles", string.Join("\n", (user as SocketGuildUser).Roles.Select(x => x.Mention)), true)
                .WithCurrentTimestamp();
            
            var embed = builder.Build();
            await Context.Channel.SendMessageAsync(null, false, embed);
        }
        
        
        
    }

    [Command("purge")]
    [RequireUserPermission(GuildPermission.ManageMessages)]
    public async Task Purge(int amount)
    {
        var messages = await Context.Channel.GetMessagesAsync(amount + 1).FlattenAsync();
        await (Context.Channel as SocketTextChannel).DeleteMessagesAsync(messages);

        var message = await Context.Channel.SendMessageAsync($"Messages Deleted Count {amount}");
        await Task.Delay(2500);
        await message.DeleteAsync();
    }

    [Command("server")]
    public async Task Server()
    {
        var builder = new EmbedBuilder()
            .WithThumbnailUrl(Context.Guild.IconUrl)
            .WithDescription("Here are the Server Information")
            .WithTitle($"{Context.Guild.Name} Information")
            .AddField("Created at", Context.Guild.CreatedAt.ToString("dd.MM.yy"), true)
            .AddField("Member Count", (Context.Guild as SocketGuild).MemberCount + " members", true);
        var embed = builder.Build();
        await Context.Channel.SendMessageAsync(null, false, embed);

    }
}