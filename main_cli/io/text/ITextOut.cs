namespace main_cli.io.text
{
    public interface ITextOut
    {
        void writeLine(string text, TextStyle style=TextStyle.Default);
        void write(string text, TextStyle style=TextStyle.Default);
        void writeLineErr(string errText);
        void writeErr(string errText);
        void newline();
    }

    public enum TextStyle
    {
        Default, 
        Error, 
    
        InternalMechanics,
        Data,
        Notes,
        DebugInfo, 
    }
}
