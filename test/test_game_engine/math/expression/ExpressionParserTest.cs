using System;
using System.Linq;
using game_engine.math.expression;
using NUnit.Framework;

namespace test_game_engine.math.expression
{
    class ExpressionParserTest
    {
        private ExpressionParser parser;

        [SetUp]
        public void Setup()
        {
            parser = new ExpressionParser();
        }

        [TestCase("2", "2")]
        [TestCase("2 + 1", "+(2,1)")]
        [TestCase("2+1", "+(2,1)")]
        [TestCase(" 2+1  ", "+(2,1)")]
        [TestCase("2 - 1", "-(2,1)")]
        [TestCase("2 * 3", "*(2,3)")]
        [TestCase("6 / 2", "/(6,2)")]
        [TestCase("2 * -3", "*(2,-(3))")]
        [TestCase("2 * +3", "*(2,+(3))")]
        [TestCase("1 + 2 * 3", "+(1,*(2,3))")]
        [TestCase("(1 + 2) * 3", "*(+(1,2),3)")]
        [TestCase("3 * (1 + 2)", "*(3,+(1,2))")]
        [TestCase("(1 + 2) * (4 + 5)", "*(+(1,2),+(4,5))")]
        [TestCase("(1 + 2 * (3 + 4)) * 5", "*(+(1,*(2,+(3,4))),5)")]
        [TestCase("d20 + 4", "+(d20,4)")]
        [TestCase("4 + d20", "+(4,d20)")]
        [TestCase("4 + 2d20", "+(4,2d20)")]

        public void validExpression(string testExpression, string expectedResult)
        {
            Assert.That(astString(parser.parseExpression(testExpression)), Is.EqualTo(expectedResult));
        }

        private static string astString(AstNode node)
        {
            if (node.args == null)
            {
                return node.text;
            }
            else
            {
                return node.text + "(" + string.Join(',', node.args.Select(astString)) +  ")";
            }
        }
    }
}
