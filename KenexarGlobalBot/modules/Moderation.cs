using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace KenexarGlobalBot.modules;

public class Moderation : ModuleBase
{
    [Command("purge")]
    [RequireUserPermission(GuildPermission.ManageMessages)]
    public async Task Purge(int amount)
    {
        var messages = await Context.Channel.GetMessagesAsync(amount + 1).FlattenAsync();
        await (Context.Channel as SocketTextChannel)!.DeleteMessagesAsync(messages);

        var message = await Context.Channel.SendMessageAsync($"Messages Deleted Count {amount}");
        await Task.Delay(2500);
        await message.DeleteAsync();
    }
}