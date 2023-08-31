[AttributeUsage(AttributeTargets.Class)]
public class Command : Attribute
{
    public string cmd_name { get; }
    public Command(string commandName)
    {
        cmd_name = commandName;
    }
}
