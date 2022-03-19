using cli_application.app;
using cli_application.cmd.commands;
using cli_application.io.text;
using Moq;
using NUnit.Framework;

namespace test_cli_application.cmd.commands
{
    public class ExitTest
    {
        private Mock<TextOut> textOut;
        private Mock<Application> app;
        private Exit exitCmd;

        [SetUp]
        public void Setup()
        {
            textOut = new Mock<TextOut>();
            app = new Mock<Application>();
            app.SetupGet(a => a.textOut).Returns(textOut.Object);
            exitCmd = new Exit();
        }

        [Test]
        public void RunCmd()
        {
            exitCmd.executeCmd("", null, app.Object);
            app.Verify(a => a.exit());
        }
    }
}