namespace main_cli.io.text
{
    public interface ITextOut
    {
        void writeLine(string text, MsgType type=MsgType.Default);
        void write(string text, MsgType type=MsgType.Default);
        void writeLineErr(string errText);
        void writeErr(string errText);
        void newline();
    }

    public enum MsgType
    {
        Default, 
        DefaultEmphasis,
        Error, 
    
        InternalMechanics,
        Data,
        Notes,
        DebugInfo, 
    }
}
