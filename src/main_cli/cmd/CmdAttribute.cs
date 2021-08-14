using System;

namespace main_cli.cmd
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
