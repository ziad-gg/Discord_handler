using System.Reflection;
using Discord.WebSocket;

public class CommandsManager
{
    public readonly Dictionary<string, Type> _commands = new Dictionary<string, Type>();
    public void Execute(string CommandName, SocketMessage message, params string[] args)
    {
        if (_commands.TryGetValue(CommandName, out var type))
        {
            var CommandBase = Activator.CreateInstance(type) as Cmd;

            if (CommandBase != null)
            {
                CommandBase?.Execute(message, args);
            }
        }
    }
    public void RegisterCommands()
    {
        var commandTypes = Assembly.GetExecutingAssembly().GetTypes()
        .Where(type => typeof(Cmd).IsAssignableFrom(type))
        .Where(type => type.GetCustomAttributes(typeof(Command), false).Length > 0);

        foreach (var type in commandTypes)
        {

            var attribute = type.GetCustomAttribute<Command>();
            if (attribute != null)
            {
                Console.WriteLine($"#{_commands.Count} Command {attribute.cmd_name} Loaded Successfully");
                Console.Beep();
                _commands.Add(attribute.cmd_name.ToLower(), type);
            }
        }


    }
}