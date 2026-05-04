using cert_ops.Contexts;
using cert_ops.Emuns;

namespace cert_ops;

public static class Program
{
    public static int Main(string[] args)
    {
        try
        {
            var ctx = CliParser.Parse(args);

            if (ctx.Verbose)
                Console.WriteLine("Verbose mode enabled");

            return Execute(ctx);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
            return 1;
        }
    }

    static int Execute(CliContext ctx)
    {
        switch (ctx.Command)
        {
            case CommandType.Version:
                Console.WriteLine("0.1.0");
                break;

            case CommandType.Help:
                PrintHelp();
                break;

            case CommandType.Status:
                Console.WriteLine("Status OK");
                break;

            case CommandType.Renew:
                Console.WriteLine("Renewing certificates...");
                break;

            case CommandType.Revoke:
                Console.WriteLine("Revoking certificate...");
                break;

            case CommandType.Config:
                ExecuteConfig(ctx);
                break;
        }

        return 0;
    }

    static void ExecuteConfig(CliContext ctx)
    {
        switch (ctx.ConfigCommand)
        {
            case ConfigCommandType.Help:
                Console.WriteLine("config init|set|get");
                break;

            case ConfigCommandType.Init:
                Console.WriteLine("Generating config...");
                break;

            case ConfigCommandType.Set:
                Console.WriteLine($"Setting: {string.Join(" ", ctx.Args)}");
                break;

            case ConfigCommandType.Get:
                Console.WriteLine($"Getting: {string.Join(" ", ctx.Args)}");
                break;

            default:
                Console.WriteLine("Invalid config command");
                break;
        }
    }

    public static void PrintHelp()
    {
        Console.WriteLine("CertOps");
        Console.WriteLine("A simple Let's Encrypt management CLI\n");

        Console.WriteLine("USAGE:");
        Console.WriteLine("  cert-ops [options] <command> [subcommand] [args]\n");

        Console.WriteLine("GLOBAL OPTIONS:");
        Console.WriteLine("  --version               Show version information");
        Console.WriteLine("  --help, -h              Show help information");
        Console.WriteLine("  --verbose, -v           Enable verbose output");
        Console.WriteLine("  --config, -c <file>     Specify configuration file\n");

        Console.WriteLine("COMMANDS:");
        Console.WriteLine("  version                 Show version information");
        Console.WriteLine("  help                    Show help information");
        Console.WriteLine("  status                  Show current certificate status");
        Console.WriteLine("  renew                   Renew certificates now");
        Console.WriteLine("  revoke                  Revoke current certificate");
        Console.WriteLine("  config                  Configuration management\n");

        Console.WriteLine("CONFIG SUBCOMMANDS:");
        Console.WriteLine("  config help             Show config help");
        Console.WriteLine("  config init             Create a new config interactively");
        Console.WriteLine("  config set <k=v>        Set a configuration value");
        Console.WriteLine("  config get <key>        Get a configuration value\n");

        Console.WriteLine("EXAMPLES:");
        Console.WriteLine("  cert-ops --version");
        Console.WriteLine("  cert-ops status");
        Console.WriteLine("  cert-ops config init");
        Console.WriteLine("  cert-ops config set acme.email=test@example.com");
        Console.WriteLine("  cert-ops -c config.yaml renew");
    }
}