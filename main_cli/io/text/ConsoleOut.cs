using System;
using System.IO;

namespace main_cli.io.text
{
    public class ConsoleOut : ITextOut
    {
        private void newline(TextWriter writer)
        {
            writer.WriteLine();
        }

        private string getStyleString(TextStyle style)
        {
            switch (style)
            {
                case TextStyle.Error:
                    return "\u001b[31m";
                case TextStyle.InternalMechanics:
                    return "\u001b[38;5;239m";
                case TextStyle.Data:
                    return "\u001b[33;1m";
                case TextStyle.Notes:
                    return "\u001b[38;5;150m";
                case TextStyle.DebugInfo:
                    return "\u001b[38;5;201m";
                default:
                    return null;
            }
        }

        private void write(string text, TextStyle style, TextWriter writer)
        {
            var styleFmt = getStyleString(style);
            if (styleFmt != null)
                writer.Write(styleFmt);
            writer.Write(text);
            if (styleFmt != null)
                writer.Write("\u001b[0m");
        }

        public void newline()
        {
            this.newline(Console.Out);
        }

        public void write(string text, TextStyle style = TextStyle.Default)
        {
            this.write(text, style, Console.Out);
        }

        public void writeLine(string text, TextStyle style = TextStyle.Default)
        {
            this.write(text, style, Console.Out);
            this.newline(Console.Out);
        }

        public void writeErr(string errText)
        {
            this.write(errText, TextStyle.Error, Console.Error);
        }

        public void writeLineErr(string errText)
        {
            this.write(errText, TextStyle.Error, Console.Error);
            this.newline(Console.Error);
        }
    }
}
