﻿using System;
using cli_application.app;
using game_engine;

namespace cli_application.cmd.commands
{
    [Cmd("roll")]
    public class Roll : Cmd
    {
        public override void executeCmd(string args, Game game, Application app)
        {
            if (string.IsNullOrEmpty(args))
            {
                throw new ArgumentException("Must provide expression for roll");
            }

            try
            {
                int result = game.expressionEvaluator.closureFromAst(game.expressionParser.parseExpression(args))
                    .Invoke(null);
                app.textOut.writeLine($"Rolled {result}");
            }
            catch (FormatException e)
            {
                app.textOut.writeLineErr(e.Message);
                app.textOut.writeLineErr("Roll failed.");
            }
            catch (OverflowException e)
            {
                app.textOut.writeLineErr(e.Message);
            }
        }

        public override string getHelpText()
        {
            return string.Join(Environment.NewLine,
                "DESCRIPTION",
                "\tEvaluates the provided math expression, rolling any dice contained in the expression. Dice rolls are",
                "\tdescribed in the format 'xdy' where 'x' is the number of dice and 'y' is the number of sides on the dice.", 
                "\tFor example, '3d20' will roll three 20-sided dice, adding the result of the rolls together. 'x' may be ",
                "\tomitted to roll one die of that type.",
                "USAGE",
                "\troll [expression]", 
                "EXAMPLES", 
                "\troll d20", 
                "\troll 2d6 + d8 + 5"
            );
        }
    }
}
