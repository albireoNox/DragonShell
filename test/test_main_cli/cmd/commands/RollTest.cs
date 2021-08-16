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
        private Mock<ExpressionParser> parser;
        private Mock<TextOut> textOut;
        private Roll rollCmd;

        [SetUp]
        public void Setup()
        {
            parser = new Mock<ExpressionParser>();
            textOut = new Mock<TextOut>();
            game = new Game(null, parser.Object);
            app = new Mock<Application>();
            app.SetupGet(a => a.textOut).Returns(textOut.Object);
            rollCmd = new Roll();
        }

        [Test]
        public void RunCmd()
        {
            parser.Setup(p => p.evaluateExpression("d20 + 1")).Returns(5);
            rollCmd.executeCmd("d20 + 1", game, app.Object);
            parser.Verify(p => p.evaluateExpression("d20 + 1"));
            textOut.Verify(t => t.writeLine("Rolled 5", MsgType.Default));
        }
    }
}