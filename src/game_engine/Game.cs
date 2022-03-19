using System;
using game_engine.game_objects;
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
        public EntityBag entities { get; init; }

        public static Game create(Random random)
        {
            var dice = new Dice(random);
            return new Game
            {
                dice = dice,
                expressionParser = new ExpressionParser(),
                expressionEvaluator = new ExpressionEvaluator(dice),
                entities = new EntityBag()
            };
        }
    }
}
