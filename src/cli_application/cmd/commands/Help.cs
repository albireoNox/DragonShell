using System;
using System.Collections.Immutable;
using cli_application.app;
using game_engine;

namespace cli_application.cmd.commands
{
    [Cmd("help")]
    public class Help : Cmd
    {
        public override void executeCmd(string args, Game game, Application app)
        {
            if (string.IsNullOrWhiteSpace(args))
            {
                app.textOut.writeLine("USAGE");
                app.textOut.writeLine("\thelp [command name]");
                app.textOut.writeLine("AVAILABLE COMMANDS");
                foreach (var cmd in app.cmdMapper.getCommandNames().ToImmutableSortedSet())
                {
                    app.textOut.writeLine("\t" + cmd);
                }
            }
            else
            {
                var cmd = app.cmdMapper.getCmd(args.Trim());
                if (cmd == null)
                {
                    app.textOut.writeLineErr($"No command named {args.Trim()}");
                }
                else
                {
                    app.textOut.writeLine(cmd.getHelpText());
                }
            }
        }

        public override string getHelpText()
        {
            return string.Join(Environment.NewLine,
                "DESCRIPTION",
                "\tProvides info on available commands. Omitting command name will print a list of all available commands",
                "USAGE", 
                "\thelp [command name]"
            );
        }
    }
}
