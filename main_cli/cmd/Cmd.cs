using main_cli.app;

namespace main_cli.cmd
{
    public abstract class Cmd
    {
        public abstract void executeCmd(IAppContext ctx, string args);
    }
}
