using System.Collections.Generic;
using System.Text;
using game_engine.errors;

namespace game_engine.math.expression
{
    /**
     * Convert a text math expression into an AST
     */
    public class ExpressionParser
    {
        private static readonly HashSet<char> NON_TOKEN_CHARS = new() { '+', '-', '*', '/', '(', ')' };
        private static readonly HashSet<string> OPERATORS = new() { "+", "-", "*", "/"};
       
        public ExpressionParser() { }

        // Unary operators implicitly have highest precedence. 
        private static readonly Dictionary<string, int> PRECEDENCE = new()
        {
            {"+", 1},
            {"-", 1},
            {"*", 2},
            {"/", 2},
            {"(", 0},
            {")", 0},
        };

        public virtual AstNode parseExpression(string expression)
        {
            Stack<PostfixToken> postfixTokens = lexToPostfix(expression);
            if (postfixTokens.Count == 0)
            {
                return null;
            }
            return buildAst(postfixTokens);
        }

        private static IEnumerable<Token> lexExpression(string expression)
        {
            StringBuilder tokenBuilder = new StringBuilder();
            for (int i = 0; i < expression.Length; i++)
            {
                char c = expression[i];
                if (char.IsLetterOrDigit(c))
                {
                    tokenBuilder.Append(c);
                }
                else
                {
                    bool isOperator = NON_TOKEN_CHARS.Contains(c);
                    if (!(isOperator || char.IsWhiteSpace(c)))
                    {
                        throw new ExpressionException($"Invalid character '{c}'.", i);
                    }

                    if (tokenBuilder.Length > 0)
                    {
                        yield return Token.create(tokenBuilder.ToString(), i);
                        tokenBuilder.Clear();
                    }

                    if (isOperator)
                    {
                        yield return Token.create(c.ToString(), i);
                    }
                }
            }

            if (tokenBuilder.Length > 0)
            {
                yield return Token.create(tokenBuilder.ToString(), expression.Length);
            }
        }

        private static Stack<PostfixToken> lexToPostfix(string expression)
        {
            Stack<PostfixToken> operators = new Stack<PostfixToken>();
            Stack<PostfixToken> postfixTokens = new Stack<PostfixToken>();
            Token? previous = null;

            foreach (Token t in lexExpression(expression))
            {
                if (t.text == "(")
                {
                    operators.Push(t.asPostfix(0));
                }
                else if (t.text == ")")
                {
                    while (operators.Peek().token.text != "(")
                    {
                        postfixTokens.Push(operators.Pop());
                    }
                    operators.Pop();
                }
                else if (t.isOp())
                {
                    if (previous?.isOp() ?? true) // Unary operator
                    {
                        operators.Push(t.asPostfix(1));
                    }
                    else
                    {
                        while (operators.Count > 0 && operators.Peek().token.precedence() > t.precedence())
                        {
                            postfixTokens.Push(operators.Pop());
                        }
                        operators.Push(t.asPostfix(2));
                    }
                }
                else
                {
                    postfixTokens.Push(t.asPostfix(0));
                }
                previous = t;
            }

            while (operators.Count > 0)
            {
                postfixTokens.Push(operators.Pop());
            }

            return postfixTokens;
        }

        private static AstNode buildAst(Stack<PostfixToken> postfixTokens)
        {
            PostfixToken ptoken = postfixTokens.Pop();
            if (ptoken.numArgs > 0)
            {
                AstNode[] args = new AstNode[ptoken.numArgs];
                for (int iArg = ptoken.numArgs - 1; iArg >= 0; iArg--)
                {
                    args[iArg] = buildAst(postfixTokens);
                }
                return AstNode.function(ptoken.token.text, args);
            }
            else
            {
                return AstNode.token(ptoken.token.text);
            }
        }

        private readonly struct Token
        {
            public string text { get; private init; }
            public int index { get; private init; }

            public static Token create(string text, int index)
            {
                return new Token { text = text, index = index};
            }

            public int precedence()
            {
                return PRECEDENCE[text];
            }

            public bool isOp()
            {
                return OPERATORS.Contains(text);
            }

            public PostfixToken asPostfix(int numArgs)
            {
                return PostfixToken.create(this, numArgs);
            }
        }

        private readonly struct PostfixToken
        {
            public Token token { get; private init; }
            public int numArgs { get; private init; }

            public static PostfixToken create(Token token, int numArgs)
            {
                return new PostfixToken { token = token, numArgs = numArgs };
            }
        }
    } 
}
