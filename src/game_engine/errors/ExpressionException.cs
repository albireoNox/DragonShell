using System;

namespace game_engine.errors
{
    public class ExpressionException : FormatException
    {
        public readonly int? index;

        public ExpressionException(string msg, int index, Exception inner) : base(msg, inner)
        {
            this.index = index;
        }
        
        public ExpressionException(string msg, int index) : base(msg)
        {
            this.index = index;
        }

        public ExpressionException(string msg) : base(msg)
        {
            this.index = null;
        }
    }
}
