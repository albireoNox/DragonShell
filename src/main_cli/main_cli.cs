using System;
using main_cli.app;
using main_cli.io.text;
using main_cli.cmd;

namespace main_cli
{
    class CliApp : IAppContext
    {
        private static readonly string PROMPT = "> ";
        private readonly CmdExecutor cmdExecutor;
        private readonly Singletons singletons;

        public ITextOut textOut { get; }
        public void exit()
        {
            Environment.Exit(0);
        }

        CliApp()
        {
            this.textOut = new ConsoleOut();
            this.singletons = new Singletons(new Random(), this);
            this.cmdExecutor = new CmdExecutor(CmdMapper.createWithMappings(this.singletons), this);
        }

        void main()
        {
            textOut.writeLine("Welcome!", MsgType.DefaultEmphasis);
            while (true)
            {
                textOut.write(PROMPT);
                cmdExecutor.executeRawCmdLine(Console.ReadLine());
            }
        }

        static void Main(string[] args)
        {
            CliApp app = new CliApp();

            app.main();
        }
    }
}
