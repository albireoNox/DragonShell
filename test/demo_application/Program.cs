using System;
using cli_application;
using cli_application.app;

namespace demo_application
{
    class Program
    {
        static void Main(string[] args)
        {
            CliApp app = new CliApp();

            app.main();
        }
    }
}
