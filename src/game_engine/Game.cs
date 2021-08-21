using System;
using game_engine.math.expression;

namespace game_engine
{ 
    /**
     * Top level object to represent the game logic and state
     */
    public class Game
    {
        public Dice dice { get; init; }
        public ExpressionParser expressionParser { get; init; }
        public ExpressionEvaluator expressionEvaluator { get; init; }

        public static Game create(Random random)
        {
            var dice = new Dice(random);
            var expressionEvaluator = new ExpressionEvaluator(dice);
            return new Game
            {
                dice = dice,
                expressionParser = new ExpressionParser(),
                expressionEvaluator = expressionEvaluator
            };
        }
    }
}
