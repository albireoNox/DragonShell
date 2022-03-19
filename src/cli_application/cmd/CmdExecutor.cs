using System;
using System.Text.RegularExpressions;
using cli_application.app;
using cli_application.io.text;
using game_engine;

namespace cli_application.cmd
{
    public class CmdExecutor
    {
        private static Regex CMD_PATTERN = new Regex(
            "^(?<cmdName>\\w+)\\s*(?<cmdArgs>.*)$",
            RegexOptions.Compiled,
            TimeSpan.FromSeconds(3.0));

        private readonly CmdMapper mapper;
        private readonly TextOut output;

        public CmdExecutor(CmdMapper mapper, TextOut output)
        {
            this.mapper = mapper;
            this.output = output;
        }

        public void executeRawCmdLine(string line, Game game, Application app)
        {
            if (line == null)
            {
                return;
            }

            var trimmedLine = line.Trim();

            if (trimmedLine.Length == 0)
            {
                return;
            }
            
            var match = CMD_PATTERN.Match(trimmedLine);
            if (!match.Success)
            {
                output.writeLineErr("Invalid command format!");
                return;
            }

            string cmdName = match.Groups["cmdName"].Value;
            string cmdArgs = match.Groups["cmdArgs"].Value;

            Cmd cmd = mapper.getCmd(cmdName);

            if (cmd == null)
            {
                output.writeLineErr($"'{cmdName}' is not a valid command.");
                return;
            }

            cmd.executeCmd(cmdArgs, game, app);
        }
    }
}
