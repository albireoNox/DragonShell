using main_cli.app;
using Moq;
using NUnit.Framework;

namespace main_cli.math.expression
{
    class ExpressionParserTest
    {
        private static readonly IAppContext CTX = NullAppContext.I;

        private Mock<Dice> dice;
        private ExpressionParser parser;

        [SetUp]
        public void Setup()
        {
            dice = new Mock<Dice>();
            parser = new ExpressionParser(dice.Object, CTX);

            dice.Setup(d => d.roll("d20")).Returns(20);
            dice.Setup(d => d.isDiceToken("d20")).Returns(true);
            dice.Setup(d => d.roll("2d20")).Returns(40);
            dice.Setup(d => d.isDiceToken("2d20")).Returns(true);
        }

        [TestCase("2", 2)]
        [TestCase("2 + 1", 3)]
        [TestCase("2+1", 3)]
        [TestCase(" 2+1  ", 3)]
        [TestCase("2 - 1", 1)]
        [TestCase("2 * 3", 6)]
        [TestCase("6 / 2", 3)]
        [TestCase("2 * -3", -6)]
        [TestCase("2 * +3", 6)]
        [TestCase("1 + 2 * 3", 7)]
        [TestCase("(1 + 2) * 3", 9)]
        [TestCase("3 * (1 + 2)", 9)]
        [TestCase("(1 + 2) * (4 + 5)", 27)]
        [TestCase("(1 + 2 * (3 + 4)) * 5", 75)]
        [TestCase("d20 + 4", 24)]
        [TestCase("4 + d20", 24)]
        [TestCase("4 + 2d20", 44)]

        public void validExpression(string testExpression, int expectedResult)
        {
            Assert.That(parser.evaluateExpression(testExpression), Is.EqualTo(expectedResult));
        } 
    }
}
