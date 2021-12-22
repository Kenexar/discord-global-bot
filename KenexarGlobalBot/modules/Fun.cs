using System;
using System.Net.Http;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Newtonsoft.Json.Linq;


namespace KenexarGlobalBot.modules;

public class Fun : ModuleBase 
{
    [Command("meme")]
    public async Task Meme()
    {
        var client = new HttpClient();
        var result = await client.GetStringAsync("https://reddit.com/r/memes/random.json?limit=1");

        var arr = JArray.Parse(result);
        var post = JObject.Parse(arr[0]["data"]["children"][0]["data"].ToString());
        
        var builder = new EmbedBuilder();
        
        builder.WithTitle(post["title"].ToString());
        builder.WithImageUrl(post["url"].ToString());
        builder.WithUrl("https://reddit.com" + post["permalink"]);
        builder.WithFooter($"🗨️ {post["num_comments"]} ⬆ {post["ups"]}");
        
        var embed = builder.Build();
        await Context.Channel.SendMessageAsync(null, false, embed);
        
    }
}