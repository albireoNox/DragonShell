using System;
using System.Collections.Generic;
using System.Text;

namespace game_engine.math.expression
{
    public class ExpressionParser
    {
        private const char UNARY_MINUS = '_';
        private static readonly HashSet<char> OPERATORS = new() { '+', '-', '*', '/', '(', ')'};
        private static readonly Dictionary<char, int> PRECEDENCE = new()
        {
            {'+', 1},
            {'-', 1},
            {'*', 2},
            {'/', 2},
            {UNARY_MINUS, 3},
            
            {'(', 0},
            {')', 0},
        };

        private readonly Dice dice;

        public ExpressionParser(Dice dice)
        {
            this.dice = dice;
        }

        public int evaluateExpression(string expression)
        {
            StringBuilder tokenBuilder = new StringBuilder();
            bool expectingToken = true; // For determining if operator is unary
            Stack<char> operators = new Stack<char>();
            Stack<int> tokens = new Stack<int>();
            foreach (char c in expression)
            {
                if (char.IsLetterOrDigit(c))
                {
                    tokenBuilder.Append(c);
                    expectingToken = false;
                } 
                else if (OPERATORS.Contains(c))
                {
                    parseAndPushToken(tokenBuilder, tokens);

                    if (expectingToken && c == '+') // Unary + is a noop
                    {
                        continue;
                    } 
                    else if (expectingToken && c == '-')
                    {
                        // Unary - is the highest precedence, so no need to check on that
                        operators.Push(UNARY_MINUS);
                    } 
                    else if (c == '(')
                    {
                        operators.Push(c);
                    }
                    else if (c == ')')
                    {
                        while (operators.Peek() != '(')
                        {
                            performOperation(tokens, operators);
                        }
                        operators.Pop();
                    } 
                    else
                    {
                        while (operators.Count > 0 && precedenceOf(operators.Peek()) > precedenceOf(c))
                        {
                            performOperation(tokens, operators);
                        }
                        operators.Push(c);
                        expectingToken = true;
                    }
                }
                else if (char.IsWhiteSpace(c))
                {
                    parseAndPushToken(tokenBuilder, tokens);
                }
                else // Invalid character
                {
                    throw new FormatException("Invalid character");
                }
            }

            parseAndPushToken(tokenBuilder, tokens);
            while (operators.Count > 0)
            {
                performOperation(tokens, operators);
            }

            if (tokens.Count == 1)
            {
                return tokens.Pop();
            }
            else
            {
                throw new FormatException("Invalid expression.");
            }

        }

        private void performOperation(Stack<int> tokens, Stack<char> operators)
        {
            int right = tokens.Pop();
            switch (operators.Pop())
            {
                case '+':
                    tokens.Push(tokens.Pop() + right);
                    break;
                case '-':
                    tokens.Push(tokens.Pop() - right);
                    break;
                case '*':
                    tokens.Push(tokens.Pop() * right);
                    break;
                case '/':
                    tokens.Push(tokens.Pop() / right);
                    break;
                case UNARY_MINUS:
                    tokens.Push(-right);
                    break;
            }
        }

        private static int precedenceOf(char op)
        {
            return PRECEDENCE[op];
        }

        private void parseAndPushToken(StringBuilder tokenBuilder, Stack<int> tokens)
        {
            if (tokenBuilder.Length > 0)
            {
                string token = tokenBuilder.ToString();
                if (dice.isDiceToken(token))
                {
                    tokens.Push(dice.roll(token));
                }
                else // It's a number
                {
                    tokens.Push(int.Parse(token));
                }
                tokenBuilder.Clear();
            }
        }
    } 
}
