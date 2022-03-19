using System;

namespace cli_application.cmd
{
    [System.AttributeUsage(System.AttributeTargets.Class)]
    class CmdAttribute : Attribute
    {
        public readonly string name;

        public CmdAttribute(string name)
        {
            this.name = name;
        }
    }
}
