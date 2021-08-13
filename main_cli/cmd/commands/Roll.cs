using System;
using main_cli.app;
using main_cli.io.text;
using main_cli.math.expression;

namespace main_cli.cmd.commands
{
    [Cmd("roll")]
    class Roll : Cmd
    {
        private readonly ExpressionParser expressionParser;
        private readonly ITextOut textOut;

        public Roll(Singletons singletons)
        {
            this.expressionParser = singletons.expressionParser;
            this.textOut = singletons.ctx.textOut;
        }

        public override void executeCmd(string args)
        {
            try
            {
                int result = expressionParser.evaluateExpression(args);
                textOut.writeLine($"Rolled a {result}");
            }
            catch (FormatException e)
            {
                textOut.writeLineErr(e.Message);
                textOut.writeLineErr("Roll failed.");
            }
        }
    }
}
