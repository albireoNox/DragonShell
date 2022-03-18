﻿using System;
using game_engine;
using game_system_rulesets.objects;
using main_cli.app;
using main_cli.io.text;
using main_cli.cmd;
using main_cli.io.file;

namespace main_cli
{
    class CliApp : Application
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

        CliApp()
        {
            this.textOut = new ConsoleOut();
            this.game = Game.create(new Random());
            this.cmdMapper = CmdMapper.createWithMappings();
            this.cmdExecutor = new CmdExecutor(cmdMapper, textOut);
        }

        void main()
        {
            textOut.writeLine("Welcome!", MsgType.DefaultEmphasis);
            while (true)
            {
                textOut.write(PROMPT);
                cmdExecutor.executeRawCmdLine(Console.ReadLine(), game, this);
            }
        }

        static void Main(string[] args)
        {
            CliApp app = new CliApp();

            Ruleset rs = RulesetIo.loadRuleset("3_5");

            app.main();
        }
    }
}
