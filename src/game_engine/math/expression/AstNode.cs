namespace game_engine.math.expression
{
    /**
     * Abstract Syntax Tree node. Can represent either a function with 0 or more arguments, or a leaf-node token. 
     */
    public class AstNode
    {
        public readonly string text;
        public readonly AstNode[] args;

        private AstNode(string text, AstNode[] args)
        {
            this.text = text;
            this.args = args;
        }

        public bool isFunction()
        {
            return args != null;
        }

        public bool isToken()
        {
            return !isFunction();
        }

        public static AstNode function(string name, AstNode[] args)
        {
            return new AstNode(name, args);
        }

        public static AstNode token(string text)
        {
            return new AstNode(text, null);
        }
    }
}
