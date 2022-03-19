using System;
using cli_application.app;
using game_engine;

namespace cli_application.cmd.commands
{
    [Cmd("exit")]
    public class Exit : Cmd
    {
        public override void executeCmd(string args, Game game, Application app)
        {
            app.textOut.writeLine("Exiting the application!");
            app.exit();
        }

        public override string getHelpText()
        {
            return string.Join(Environment.NewLine,
                "DESCRIPTION",
                "\tExits the application.",
                "USAGE",
                "\texit"
            );
        }
    }
}
