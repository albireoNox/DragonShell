using System;
using main_cli.app;
using main_cli.io.text;
using main_cli.cmd;

namespace main_cli
{
    class CliApp : IAppContext
    {
        private readonly CmdExecutor cmdExecutor;

        public ITextOut textOut { get; }
        public void exit()
        {
            Environment.Exit(0);
        }

        CliApp()
        {
            this.textOut = new ConsoleOut();
            this.cmdExecutor = new CmdExecutor(CmdMapper.createWithMappings());
        }

        void main()
        {
            while (true)
            {
                cmdExecutor.executeRawCmdLine(Console.ReadLine(), this);
            }
        }

        static void Main(string[] args)
        {
            CliApp app = new CliApp();

            app.main();

            app.textOut.writeLine("This is text");
            app.textOut.writeLineErr("This is error text");
            app.textOut.writeLine("This is dice text", MsgType.InternalMechanics);
            app.textOut.writeLine("This is data", MsgType.Data);
            app.textOut.writeLine("This is notes", MsgType.Notes);
            app.textOut.writeLine("This is debug info", MsgType.DebugInfo);
        }
    }
}
