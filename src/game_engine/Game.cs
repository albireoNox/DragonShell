using System;
using game_engine.math.expression;

namespace game_engine
{ 
    /**
     * Top level object to represent the game logic and state
     */
    public class Game
    {
        public Dice dice;
        public ExpressionParser expressionParser;

        public Game() { } // Test mocks need this

        public Game(Dice dice, ExpressionParser expressionParser)
        {
            this.dice = dice;
            this.expressionParser = expressionParser;
        }

        public static Game create(Random random)
        {
            var dice = new Dice(random);
            var expressionParser = new ExpressionParser(dice);
            return new Game(dice, expressionParser);
        }
    }
}
