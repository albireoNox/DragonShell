namespace cli_application.io.text
{
    class NullOut : TextOut
    {
        public static readonly NullOut I = new NullOut();

        public void writeLine(string text, MsgType type = MsgType.Default) { }

        public void write(string text, MsgType type = MsgType.Default) { }

        public void writeLineErr(string errText) { }

        public void writeErr(string errText) { }

        public void newline() { }
    }
}
