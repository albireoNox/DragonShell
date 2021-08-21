using System;
using game_engine.errors;

namespace game_engine.math.expression
{
    public class ExpressionEvaluator
    {
        private readonly Dice dice;

        public ExpressionEvaluator() { } // Test mocks need this

        public ExpressionEvaluator(Dice dice)
        {
            this.dice = dice;
        }

        public virtual Func<ExpressionContext, ExpressionResult> closureFromAst(AstNode astNode)
        {
            if (astNode.isToken())
            {
                return mapToken(astNode.text);
            }
            else
            {
                return mapFunc(astNode);
            }
        }

        private Func<ExpressionContext, ExpressionResult> mapToken(string text)
        {
            if (dice.isDiceToken(text))
            {
                return ctx => dice.roll(text);
            }
            else
            {
                return ctx => int.Parse(text);
            }
        }

        private Func<ExpressionContext, ExpressionResult> mapFunc(AstNode astNode)
        {
            switch (astNode.args.Length)
            {
                case 1:
                    AstNode arg = astNode.args[0];
                    switch (astNode.text)
                    {
                        case "+": // NOOP
                            return closureFromAst(arg);
                        case "-":
                            var argClosure = closureFromAst(arg);
                            return ctx => -argClosure.Invoke(ctx);
                    }
                    break;
                case 2:
                    var leftArg = closureFromAst(astNode.args[0]);
                    var rightArg = closureFromAst(astNode.args[1]);
                    switch (astNode.text)
                    {
                        case "+":
                            return ctx => leftArg.Invoke(ctx) + rightArg.Invoke(ctx);
                        case "-":
                            return ctx => leftArg.Invoke(ctx) - rightArg.Invoke(ctx);
                        case "*":
                            return ctx => leftArg.Invoke(ctx) * rightArg.Invoke(ctx);
                        case "/":
                            return ctx => leftArg.Invoke(ctx) / rightArg.Invoke(ctx);
                    }
                    break;
            }

            throw new ExpressionException(
                $"Could not map function '{astNode.text}' with '{astNode.args.Length}' argument(s).");
        }
    }
}
