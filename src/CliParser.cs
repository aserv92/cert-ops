using cert_ops.Contexts;
using cert_ops.Emuns;

namespace cert_ops;

public static class CliParser
{
    public static CliContext Parse(string[] args)
    {
        var ctx = new CliContext();

        if (args.Length == 0)
        {
            ctx.Command = CommandType.Help;
            return ctx;
        }

        int i = 0;

        // -------- GLOBAL FLAGS --------
        while (i < args.Length && args[i].StartsWith("-"))
        {
            switch (args[i])
            {
                case "--version":
                case "version":
                    ctx.Command = CommandType.Version;
                    return ctx;

                case "--help":
                case "-h":
                case "help":
                    ctx.Command = CommandType.Help;
                    return ctx;

                case "--verbose":
                case "-v":
                    ctx.Verbose = true;
                    break;

                case "--config":
                case "-c":
                    if (i + 1 >= args.Length)
                        throw new Exception("--config requires a file path");

                    ctx.ConfigFile = args[++i];
                    break;

                default:
                    throw new Exception($"Unknown option: {args[i]}");
            }

            i++;
        }

        if (i >= args.Length)
        {
            ctx.Command = CommandType.Help;
            return ctx;
        }

        // -------- ROOT COMMAND --------
        var command = args[i++].ToLower();

        switch (command)
        {
            case "version":
                ctx.Command = CommandType.Version;
                return ctx;

            case "help":
                ctx.Command = CommandType.Help;
                return ctx;

            case "status":
                ctx.Command = CommandType.Status;
                return ctx;

            case "renew":
                ctx.Command = CommandType.Renew;
                return ctx;

            case "revoke":
                ctx.Command = CommandType.Revoke;
                return ctx;

            case "config":
                ctx.Command = CommandType.Config;
                break;

            default:
                throw new Exception($"Unknown command: {command}");
        }

        // -------- CONFIG SUBCOMMAND --------
        if (ctx.Command == CommandType.Config)
        {
            if (i >= args.Length)
            {
                ctx.ConfigCommand = ConfigCommandType.Help;
                return ctx;
            }

            var sub = args[i++].ToLower();

            switch (sub)
            {
                case "help":
                    ctx.ConfigCommand = ConfigCommandType.Help;
                    break;

                case "init":
                    ctx.ConfigCommand = ConfigCommandType.Init;
                    break;

                case "set":
                    ctx.ConfigCommand = ConfigCommandType.Set;
                    ctx.Args = args.Skip(i).ToArray();

                    if (ctx.Args.Length == 0)
                        throw new Exception("config set requires key=value");

                    break;

                case "get":
                    ctx.ConfigCommand = ConfigCommandType.Get;
                    ctx.Args = args.Skip(i).ToArray();

                    if (ctx.Args.Length == 0)
                        throw new Exception("config get requires key");

                    break;

                default:
                    throw new Exception($"Unknown config command: {sub}");
            }
        }

        return ctx;
    }
}
