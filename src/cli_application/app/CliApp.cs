using System;
using System.IO;
using cli_application.cmd;
using cli_application.io.text;
using game_engine;
using game_system_rulesets.io;
using game_system_rulesets.objects;

namespace cli_application.app
{
    public class CliApp : Application
    {
        private static readonly string PROMPT = "> ";
        private readonly CmdExecutor cmdExecutor;
        private readonly Game game;

        public CmdMapper cmdMapper { get; }
        public TextOut textOut { get; }
        public void exit()
        {
            Environment.Exit(0);
        }

        public CliApp(FileInfo saveFile=null)
        {
            this.textOut = new ConsoleOut();
            this.cmdMapper = CmdMapper.createWithMappings();
            this.cmdExecutor = new CmdExecutor(cmdMapper, textOut);
            this.game = initGame(saveFile);
        }

        private Game initGame(FileInfo saveFile)
        {
            textOut.writeLine("Loading rule set 3.5e...");
            Ruleset rs = RulesetIo.loadRuleset("3_5");
            textOut.writeLine("Loading rule set 3.5e...");

            if (saveFile == null)
            {
                throw new NotImplementedException();
            }
            else
            {
                // TODO
            }

            return Game.create(new Random());
        }

        public void main()
        {
            textOut.writeLine("Welcome!", MsgType.DefaultEmphasis);
            while (true)
            {
                textOut.write(PROMPT);

                try
                {
                    cmdExecutor.executeRawCmdLine(Console.ReadLine(), game, this);
                }
                catch (Exception e)
                {
                    textOut.writeLineErr(e.Message);
#if DEBUG
                    textOut.writeErr(e.StackTrace + Environment.NewLine);
#endif
                }
            }
        }
    }
}
