using cli_application.cmd;
using cli_application.io.text;

namespace cli_application.app
{
    /**
     * Top level interface to manage the application separate from game state (such and windows, io, os interfaces, etc.)
     */
    public interface Application
    {
        public CmdMapper cmdMapper { get; }
        public TextOut textOut { get; }
        public void exit();
    }
}
