using main_cli.io.text;

namespace main_cli.app
{
    public class NullAppContext : IAppContext
    {
        public static readonly NullAppContext I = new NullAppContext();

        public ITextOut textOut => NullOut.I;

        public void exit() { }

        private NullAppContext() {}
    }
}
