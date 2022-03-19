using cli_application.app;
using cli_application.cmd;
using cli_application.cmd.commands;
using cli_application.io.text;
using Moq;
using NUnit.Framework;

namespace test_cli_application.cmd.commands
{
    public class HelpTest
    {
        private Mock<TextOut> textOut;
        private Mock<CmdMapper> cmdMapper;
        private Mock<Application> app;
        private Mock<Cmd> cmd;
        private Help helpCmd;

        [SetUp]
        public void Setup()
        {
            textOut = new Mock<TextOut>();
            cmdMapper = new Mock<CmdMapper>();
            cmd = new Mock<Cmd>();
            app = new Mock<Application>();
            app.SetupGet(a => a.cmdMapper).Returns(cmdMapper.Object);
            app.SetupGet(a => a.textOut).Returns(textOut.Object);
            helpCmd = new Help();
        }

        [TestCase("roll")]
        [TestCase("    roll ")]
        public void RunCmdWithArg(string args)
        {
            cmdMapper.Setup(m => m.getCmd("roll")).Returns(cmd.Object);
            cmd.Setup(c => c.getHelpText()).Returns("HELP TEXT");
            helpCmd.executeCmd(args, null, app.Object);
            cmdMapper.Verify(m => m.getCmd("roll"));
            textOut.Verify(t => t.writeLine("HELP TEXT", MsgType.Default));
        }
    }
}