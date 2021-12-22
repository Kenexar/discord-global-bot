﻿using Discord.Commands;
using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;

namespace KenexarGlobalBot.services;

public class CommandHandler
{   
    public static IServiceProvider _provider;
    public static DiscordSocketClient _discord;
    public static CommandService _commands;
    public static IConfigurationRoot _config;
    
    public CommandHandler(DiscordSocketClient discord, CommandService commands, IConfigurationRoot config,
        IServiceProvider provider)
    {
        _provider = provider;
        _config = config;
        _discord = discord;
        _commands = commands;
        
        _discord.Ready += OnReady;
        _discord.MessageReceived += OnMessageReceived;
    }

    private async Task OnMessageReceived(SocketMessage arg)
    {
        var msg = arg as SocketUserMessage;

        if (msg.Author.IsBot) return;
        var context = new SocketCommandContext(_discord, msg);
        
        int pos = 0;
        if (msg.HasStringPrefix(_config["prefix"], ref pos) || msg.HasMentionPrefix(_discord.CurrentUser, ref pos))
        {
            var result = await _commands.ExecuteAsync(context, pos, _provider);

            if (!result.IsSuccess)
            {
                var reason = result.Error;

                await context.Channel.SendMessageAsync($"The Following command raised an Error: \n{reason}");
            }
        }
    }

    private Task OnReady()
    {
        Console.WriteLine($"Connected as {_discord.CurrentUser.Username}#{_discord.CurrentUser.Discriminator}");
        return Task.CompletedTask;
    }
}