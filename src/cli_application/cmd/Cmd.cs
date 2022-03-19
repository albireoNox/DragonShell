using cli_application.app;
using game_engine;

namespace cli_application.cmd
{
    public abstract class Cmd
    {
        public abstract void executeCmd(string args, Game game, Application application);

        public abstract string getHelpText();
    }
}
