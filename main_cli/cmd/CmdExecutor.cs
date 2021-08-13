using System;
using System.Text.RegularExpressions;
using main_cli.app;

namespace main_cli.cmd
{
    public class CmdExecutor
    {
        private static Regex CMD_PATTERN = new Regex(
            "^(?<cmdName>\\w+)\\s*(?<cmdArgs>.*)$",
            RegexOptions.Compiled,
            TimeSpan.FromSeconds(3.0));

        private readonly CmdMapper mapper;

        public CmdExecutor(CmdMapper mapper)
        {
            this.mapper = mapper;
        }

        public void executeRawCmdLine(string line, IAppContext ctx)
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
                ctx.textOut.writeLineErr("Invalid command format!");
                return;
            }

            string cmdName = match.Groups["cmdName"].Value;
            string cmdArgs = match.Groups["cmdArgs"].Value;

            Cmd cmd = mapper.getCmd(cmdName);

            if (cmd == null)
            {
                ctx.textOut.writeLineErr($"'{cmdName}' is not a valid command.");
                return;
            }

            cmd.executeCmd(ctx, cmdArgs);
        }
    }
}
