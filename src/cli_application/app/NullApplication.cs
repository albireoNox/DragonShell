using cli_application.cmd;
using cli_application.io.text;

namespace cli_application.app
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
