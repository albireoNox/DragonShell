using main_cli.app;

namespace main_cli.cmd.commands
{
    [Cmd("roll")]
    class Roll : Cmd
    {
        public override void executeCmd(IAppContext ctx, string args)
        {
            ctx.textOut.writeLine("ROLLING!!");
        }
    }
}
