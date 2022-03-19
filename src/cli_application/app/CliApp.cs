using System;
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

        public CliApp()
        {
            this.textOut = new ConsoleOut();
            this.game = Game.create(new Random());
            this.cmdMapper = CmdMapper.createWithMappings();
            this.cmdExecutor = new CmdExecutor(cmdMapper, textOut);
        }

        public void main()
        {
            Ruleset rs = RulesetIo.loadRuleset("3_5");
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
