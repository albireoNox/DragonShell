using System;
using game_engine.math.expression;

namespace game_engine
{ 
    /**
     * Top level object to represent the game logic and state
     */
    public class Game
    {
        public readonly Dice dice;
        public readonly ExpressionParser expressionParser;

        public Game() { } // Test mocks need this

        public Game(Random random)
        {
            this.dice = new Dice(random);
            this.expressionParser = new ExpressionParser(this.dice);
        }
    }
}
