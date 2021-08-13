using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using main_cli.app;

namespace main_cli.cmd.commands
{
    [Cmd("exit")]
    class Exit : Cmd
    {
        public override void executeCmd(IAppContext ctx, string args)
        {
            ctx.textOut.writeLine("Exiting the application!");
            ctx.exit();
        }
    }
}
