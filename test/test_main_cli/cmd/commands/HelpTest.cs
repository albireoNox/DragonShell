using main_cli.app;
using main_cli.cmd;
using main_cli.cmd.commands;
using main_cli.io.text;
using Moq;
using NUnit.Framework;

namespace test_main_cli.cmd.commands
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