using main_cli.app;

namespace main_cli.cmd.commands
{
    [Cmd("exit")]
    class Exit : Cmd
    {
        private readonly IAppContext ctx;

        public Exit(Singletons singletons)
        {
            this.ctx = singletons.ctx;
        }

        public override void executeCmd(string args)
        {
            ctx.textOut.writeLine("Exiting the application!");
            ctx.exit();
        }
    }
}
