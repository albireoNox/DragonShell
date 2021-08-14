using System;
using game_engine;
using main_cli.app;

namespace main_cli.cmd.commands
{
    [Cmd("roll")]
    class Roll : Cmd
    {
        public override void executeCmd(string args, Game game, Application app)
        {
            try
            {
                int result = game.expressionParser.evaluateExpression(args);
                app.textOut.writeLine($"Rolled {result}");
            }
            catch (FormatException e)
            {
                app.textOut.writeLineErr(e.Message);
                app.textOut.writeLineErr("Roll failed.");
            }
        }
    }
}
