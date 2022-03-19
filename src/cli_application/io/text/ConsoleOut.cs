using System;
using System.IO;

namespace cli_application.io.text
{
    public class ConsoleOut : TextOut
    {
        private void newline(TextWriter writer)
        {
            writer.WriteLine();
        }

        private string getStyleString(MsgType type)
        {
            switch (type)
            {
                case MsgType.DefaultEmphasis:
                    return getStyleString(MsgType.Default) + "\u001b[4m";
                case MsgType.Error:
                    return "\u001b[31m";
                case MsgType.InternalMechanics:
                    return "\u001b[38;5;239m";
                case MsgType.Data:
                    return "\u001b[33;1m";
                case MsgType.Notes:
                    return "\u001b[38;5;150m";
                case MsgType.DebugInfo:
                    return "\u001b[38;5;201m";
                default:
                    return null;
            }
        }

        private void write(string text, MsgType type, TextWriter writer)
        {
            var styleFmt = getStyleString(type);
            if (styleFmt != null)
                writer.Write(styleFmt);
            writer.Write(text.Replace("\t", "    "));
            if (styleFmt != null)
                writer.Write("\u001b[0m");
        }

        public void newline()
        {
            this.newline(Console.Out);
        }

        public void write(string text, MsgType type = MsgType.Default)
        {
            this.write(text, type, Console.Out);
        }

        public void writeLine(string text, MsgType type = MsgType.Default)
        {
            this.write(text, type, Console.Out);
            this.newline(Console.Out);
        }

        public void writeErr(string errText)
        {
            this.write(errText, MsgType.Error, Console.Error);
        }

        public void writeLineErr(string errText)
        {
            this.write(errText, MsgType.Error, Console.Error);
            this.newline(Console.Error);
        }
    }
}
