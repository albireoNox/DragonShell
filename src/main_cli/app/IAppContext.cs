using main_cli.io.text;

namespace main_cli.app
{
    /**
     * In this case "context" means whether the running application is a CLI app, windowed app, REST service, etc. This class
     * maintains context-specific resources such as loggers and IO handlers for use by context-agnostic logic. 
     */
    public interface IAppContext
    {
        public ITextOut textOut { get; }
        public void exit();
    }
}
