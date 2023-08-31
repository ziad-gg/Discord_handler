using Discord;
using Discord.Rest;
using Discord.WebSocket;

abstract class Cmd
{
    public virtual async Task<object> Execute(SocketMessage message, string[] args)
    {
        await Task.CompletedTask;
        return Task.CompletedTask;
    }
}
static class SocketMessageExtensions
{
    public static async Task<RestUserMessage?> replyNoMention(this SocketMessage message, string text, Embed? embed = null, MessageComponent? components = null)
    {
        if (message.Channel is SocketGuildChannel guildChannel)
        {
            var guild = guildChannel.Guild;
            var Ref = new MessageReference(message.Id, message.Channel.Id, guild.Id, false);
            var mess = await message.Channel.SendMessageAsync(text, messageReference: Ref, allowedMentions: AllowedMentions.None);
            return mess ?? null;
        }
        return null;
    }
}