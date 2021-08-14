using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace main_cli.io.text
{
    class NullOut : ITextOut
    {
        public static readonly NullOut I = new NullOut();

        public void writeLine(string text, MsgType type = MsgType.Default) { }

        public void write(string text, MsgType type = MsgType.Default) { }

        public void writeLineErr(string errText) { }

        public void writeErr(string errText) { }

        public void newline() { }
    }
}
