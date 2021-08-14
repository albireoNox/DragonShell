using System;
using game_engine.math.expression;

namespace main_cli.app
{
    public class Singletons
    {
        public readonly Dice dice;
        public readonly ExpressionParser expressionParser;
        public readonly IAppContext ctx;

        public Singletons(Random random, IAppContext ctx)
        {
            this.dice = new Dice(random);
            this.expressionParser = new ExpressionParser(dice);
            this.ctx = ctx;
        }
    }
}
