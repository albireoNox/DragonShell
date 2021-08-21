using game_engine;
using game_engine.math.expression;
using main_cli.app;
using main_cli.cmd.commands;
using main_cli.io.text;
using Moq;
using NUnit.Framework;

namespace test_main_cli.cmd.commands
{
    public class RollTest
    {
        private Game game;
        private Mock<Application> app;
        private Mock<ExpressionEvaluator> evaluator;
        private Mock<ExpressionParser> parser;
        private AstNode ast;
        private Mock<TextOut> textOut;
        private Roll rollCmd;

        [SetUp]
        public void Setup()
        {
            evaluator = new Mock<ExpressionEvaluator>();
            parser = new Mock<ExpressionParser>();
            textOut = new Mock<TextOut>();
            ast = AstNode.token("test");
            game = new Game()
            {
                dice = null,
                expressionParser = parser.Object,
                expressionEvaluator = evaluator.Object
            };
            app = new Mock<Application>();
            app.SetupGet(a => a.textOut).Returns(textOut.Object);
            rollCmd = new Roll();
        }

        [Test]
        public void RunCmd()
        {
            parser.Setup(p => p.parseExpression("d20 + 1")).Returns(ast);
            evaluator.Setup(e => e.closureFromAst(ast)).Returns(ctx => 5);
            rollCmd.executeCmd("d20 + 1", game, app.Object);
            parser.Verify(p => p.parseExpression("d20 + 1"));
            evaluator.Verify(e => e.closureFromAst(ast));
            textOut.Verify(t => t.writeLine("Rolled 5", MsgType.Default));
        }
    }
}