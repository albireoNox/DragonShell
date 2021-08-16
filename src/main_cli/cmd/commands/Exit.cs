using System;
using game_engine;
using main_cli.app;

namespace main_cli.cmd.commands
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
