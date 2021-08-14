using game_engine;
using main_cli.app;

namespace main_cli.cmd.commands
{
    [Cmd("exit")]
    class Exit : Cmd
    {
        public override void executeCmd(string args, Game game, Application app)
        {
            app.textOut.writeLine("Exiting the application!");
            app.exit();
        }
    }
}
