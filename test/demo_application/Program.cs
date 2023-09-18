using cli_application.app;
using common.io;

namespace demo_application
{
    class Program
    {
        static void Main(string[] args)
        {
            var saveFile = FileSystem.currentDirectory().getSubdirectory("test_data").getFile("test_data.xml");

            CliApp app = new CliApp(saveFile);

            app.main();
        }
    }
}
