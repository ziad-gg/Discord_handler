using Discord;
using Discord.WebSocket;

#nullable enable

class Program
{
    protected static string prefix = "!";
    protected static string token = "your_token_here";
    static CommandsManager commandsManager = new CommandsManager();
    static async Task Main(string[] args)
    {
        commandsManager.RegisterCommands();

        var config = new DiscordSocketConfig
        {
            AlwaysDownloadUsers = true,
            LogLevel = LogSeverity.Error,
            GatewayIntents = GatewayIntents.All,
        };

        var client = new DiscordSocketClient(config);

        client.MessageReceived += MessageCreate;
        client.Ready += () => Ready(client);
        client.Log += Log;

        await client.LoginAsync(TokenType.Bot, token);
        await client.StartAsync();
        await Task.Delay(-1);
    }
    static Task Log(LogMessage msg)
    {
        Console.WriteLine(msg.ToString());
        return Task.CompletedTask;
    }
    static async Task<Object> Ready(DiscordSocketClient client)
    {
        await Task.Delay(1);
        Console.WriteLine($"{client.CurrentUser.Username} is ready");
        return Task.CompletedTask;
    }
    static async Task<Object?>? MessageCreate(SocketMessage message)
    {
        // Console.WriteLine(message.Content.StartsWith(prefix));
        // Console.WriteLine(!message.Content.StartsWith(prefix));
        if (!message.Content.StartsWith(prefix)) return null;

        var args = message.Content.Substring(prefix.Length).Split(" ");
        var cmd = args[0].ToLower();

        commandsManager.Execute(cmd, message, args);
        await Task.Delay(1);
        return Task.CompletedTask;
    }

}
