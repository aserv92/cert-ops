using cert_ops.Emuns;

namespace cert_ops.Contexts;

public class CliContext
{
    public bool Verbose { get; set; }
    public string? ConfigFile { get; set; }

    public CommandType Command { get; set; } = CommandType.None;

    public ConfigCommandType ConfigCommand { get; set; } = ConfigCommandType.None;

    public string[] Args { get; set; } = [];
}
