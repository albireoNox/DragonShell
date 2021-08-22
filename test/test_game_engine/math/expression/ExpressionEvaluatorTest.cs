using game_engine.math.expression;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Moq;
using NUnit.Framework;

namespace test_game_engine.math.expression
{
    class ExpressionEvaluatorTest
    {
        private Mock<Dice> dice;
        private ExpressionEvaluator evaluator;

        [SetUp]
        public void Setup()
        {
            dice = new Mock<Dice>();
            evaluator = new ExpressionEvaluator(dice.Object);

            dice.Setup(d => d.isDiceToken("d20")).Returns(true);
            dice.Setup(d => d.roll("d20")).Returns(1);
        }

        private void runTest(AstNode ast, int expectedResult)
        {
            Assert.That(evaluator.closureFromAst(ast).Invoke(null).intVal, Is.EqualTo(expectedResult));
        }

        [Test]
        public void validExpressionNumber()
        {
            runTest(T("5"), 5);
        }
        
        [Test]
        public void validExpressionRoll()
        {
            runTest(T("d20"), 1);
        }

        [Test]
        public void validExpressionAdd()
        {
            runTest(F("+", T("2"), T("3")), 5);
        }
        
        [Test]
        public void validExpressionMul()
        {
            runTest(F("*", T("2"), T("3")), 6);
        }
        
        [Test]
        public void validExpressionSub()
        {
            runTest(F("-", T("3"), T("2")), 1);
        }

        [Test]
        public void validExpressionDiv()
        {
            runTest(F("/", T("6"), T("2")), 3);
        }
        
        [Test]
        public void validExpressionComplex()
        {
            runTest(
                F("*", 
                    F("+", 
                        T("d20"), 
                        F("-", 
                            T("5"), 
                            T("2"))), 
                    T("2")), 
                8);
        }

        private static AstNode F(string name, params AstNode[] args)
        {
            return AstNode.function(name, args);
        }

        private static AstNode T(string text)
        {
            return AstNode.token(text);
        }
    }
}
