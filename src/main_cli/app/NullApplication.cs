using main_cli.cmd;
using main_cli.io.text;

namespace main_cli.app
{
    public class NullApplication : Application
    {
        public static readonly NullApplication I = new NullApplication();

        public CmdMapper cmdMapper => null;
        public TextOut textOut => NullOut.I;

        public void exit() { }

        private NullApplication() {}
    }
}
