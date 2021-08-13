using main_cli.app;
using main_cli.cmd;
using Moq;
using NUnit.Framework;

namespace test_main_cli.cmd
{
    public class CmdExecutorTest
    {
        private static readonly string CMD_NAME = "test";
        private static readonly IAppContext CTX = NullAppContext.I;

        private Mock<Cmd> testCmd;
        private Mock<CmdMapper> mapper;
        private CmdExecutor executor;

        [SetUp]
        public void Setup()
        {
            mapper = new Mock<CmdMapper>();
            testCmd = new Mock<Cmd>();
            executor = new CmdExecutor(mapper.Object);

            mapper.Setup(m => m.getCmd(CMD_NAME)).Returns(testCmd.Object);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("    ")]
        [TestCase("  \t   \n ")]
        public void NoopInputs(string input)
        {
            executor.executeRawCmdLine(input, CTX);
            mapper.VerifyNoOtherCalls();
            mapper.VerifyNoOtherCalls();
        }

        [TestCase("@@@")]
        public void InvalidFormats(string input)
        {
            executor.executeRawCmdLine(input, CTX);
            mapper.VerifyNoOtherCalls();
            mapper.VerifyNoOtherCalls();
        }

        [Test]
        public void InvalidCommand()
        {
            mapper.Setup(m => m.getCmd("invalid")).Returns((Cmd)null);
            executor.executeRawCmdLine("invalid", CTX);
            mapper.Verify(m => m.getCmd("invalid"));
            mapper.VerifyNoOtherCalls();
        }

        [TestCase("test")]
        [TestCase("test ")]
        [TestCase(" test")]
        [TestCase(" test  ")]
        public void InvokeCommandNoArgs(string input)
        {
            executor.executeRawCmdLine(input, NullAppContext.I);
            mapper.Verify(m => m.getCmd(CMD_NAME));
            testCmd.Verify(c => c.executeCmd(""));
        }
        
        [TestCase("test a", "a")]
        [TestCase("test     a", "a")]
        [TestCase("   test   a ", "a")]
        [TestCase("test  a b  c  ", "a b  c")]
        public void InvokeCommandWithArgs(string input, string expectedArgs)
        {
            executor.executeRawCmdLine(input, NullAppContext.I);
            mapper.Verify(m => m.getCmd(CMD_NAME));
            testCmd.Verify(c => c.executeCmd(expectedArgs));
        }
    }
}