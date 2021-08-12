using main_cli.io.text;
using System;

namespace main_cli
{
    class Program
    {
        static void Main(string[] args)
        {
            ITextOut output = new ConsoleOut();
            output.writeLine("This is text");
            output.writeLineErr("This is error text");
            output.writeLine("This is dice text", TextStyle.InternalMechanics);
            output.writeLine("This is data", TextStyle.Data);
            output.writeLine("This is notes", TextStyle.Notes);
            output.writeLine("This is debug info", TextStyle.DebugInfo);
        }
    }
}
