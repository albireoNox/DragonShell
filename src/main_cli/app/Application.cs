using main_cli.cmd;
using main_cli.io.text;

namespace main_cli.app
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
