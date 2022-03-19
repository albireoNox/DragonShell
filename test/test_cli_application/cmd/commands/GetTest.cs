using cli_application.app;
using cli_application.cmd.commands;
using cli_application.io.text;
using game_engine;
using game_engine.game_objects;
using Moq;
using NUnit.Framework;

namespace test_cli_application.cmd.commands
{
    public class GetTest
    {
        private Mock<TextOut> textOut;
        private Mock<Application> app;
        private Mock<EntityBag> entities;
        private Game game;
        private Get getCmd;

        [SetUp]
        public void Setup()
        {
            textOut = new Mock<TextOut>();
            app = new Mock<Application>();
            app.SetupGet(a => a.textOut).Returns(textOut.Object);
            entities = new Mock<EntityBag>();
            game = new Game {entities = entities.Object};
            getCmd = new Get();
        }

        [Test]
        public void RunCmd()
        {
            entities.Setup(e => e.findAttribute("A", new[] {"B"}))
                .Returns(new NumericAttribute("test", 2.0));
            getCmd.executeCmd("a.b", game, app.Object);
            textOut.Verify(t => t.writeLine("2", MsgType.Default));
        }
    }
}
