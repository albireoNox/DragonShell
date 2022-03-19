using cli_application.app;
using cli_application.cmd;
using cli_application.io.text;
using game_engine;
using Moq;
using NUnit.Framework;

namespace test_cli_application.cmd
{
    public class CmdExecutorTest
    {
        private static readonly string CMD_NAME = "test";

        private Mock<Cmd> testCmd;
        private Mock<CmdMapper> mapper;
        private Mock<Game> game;
        private Mock<Application> app;
        private Mock<TextOut> output;
        private CmdExecutor executor;

        [SetUp]
        public void Setup()
        {
            testCmd = new Mock<Cmd>();
            mapper = new Mock<CmdMapper>();
            game = new Mock<Game>();
            app = new Mock<Application>();
            output = new Mock<TextOut>();
            executor = new CmdExecutor(mapper.Object, output.Object);

            mapper.Setup(m => m.getCmd(CMD_NAME)).Returns(testCmd.Object);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("    ")]
        [TestCase("  \t   \n ")]
        public void NoopInputs(string input)
        {
            executor.executeRawCmdLine(input, game.Object, app.Object);
            mapper.VerifyNoOtherCalls();
            mapper.VerifyNoOtherCalls();
        }

        [TestCase("@@@")]
        public void InvalidFormats(string input)
        {
            executor.executeRawCmdLine(input, game.Object, app.Object);
            mapper.VerifyNoOtherCalls();
            mapper.VerifyNoOtherCalls();
        }

        [Test]
        public void InvalidCommand()
        {
            mapper.Setup(m => m.getCmd("invalid")).Returns((Cmd)null);
            executor.executeRawCmdLine("invalid", game.Object, app.Object);
            mapper.Verify(m => m.getCmd("invalid"));
            mapper.VerifyNoOtherCalls();
        }

        [TestCase("test")]
        [TestCase("test ")]
        [TestCase(" test")]
        [TestCase(" test  ")]
        public void InvokeCommandNoArgs(string input)
        {
            executor.executeRawCmdLine(input, game.Object, app.Object);
            mapper.Verify(m => m.getCmd(CMD_NAME));
            testCmd.Verify(c => c.executeCmd("", game.Object, app.Object));
        }
        
        [TestCase("test a", "a")]
        [TestCase("test     a", "a")]
        [TestCase("   test   a ", "a")]
        [TestCase("test  a b  c  ", "a b  c")]
        public void InvokeCommandWithArgs(string input, string expectedArgs)
        {
            executor.executeRawCmdLine(input, game.Object, app.Object);
            mapper.Verify(m => m.getCmd(CMD_NAME));
            testCmd.Verify(c => c.executeCmd(expectedArgs, game.Object, app.Object));
        }
    }
}