using Discord.WebSocket;

[Command("ping")]
class Ping : Cmd
{
    public override async Task<object> Execute(SocketMessage message, string[] args)
    {
        var content = (args.Length >= 2 && args[1] is not null) ? args[1] : "pong";

        await message.replyNoMention(content);
        return Task.CompletedTask;
    }
}
