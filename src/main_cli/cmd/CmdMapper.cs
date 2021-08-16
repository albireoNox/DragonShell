using System;
using System.Collections.Generic;
using System.Reflection;

namespace main_cli.cmd
{
    public class CmdMapper
    {
        private readonly Dictionary<string, Cmd> cmdMap;

        public CmdMapper() { } // Test mocks need this

        private CmdMapper(Dictionary<string, Cmd> cmdMap)
        {
            this.cmdMap = cmdMap;
        }

        public virtual Cmd getCmd(string name)
        {
            return cmdMap.GetValueOrDefault(name, null);
        }

        public virtual IEnumerable<string> getCommandNames()
        {
            return this.cmdMap.Keys;
        }

        public static CmdMapper createWithMappings()
        {
            var cmdMap = new Dictionary<string, Cmd>();

            var a = Assembly.GetExecutingAssembly();
            foreach (var type in a.GetTypes())
            {
                var attributes = type.GetCustomAttributes(typeof(CmdAttribute), false);
                if (attributes.Length > 0)
                {
                    CmdAttribute cmdAttribute = (CmdAttribute)attributes[0];
                    ConstructorInfo ctor = type.GetConstructor(new Type[] { });
                    if (ctor == null)
                    {
                        throw new ApplicationException("Could not find public constructor for type " + type.FullName);
                    }
                    var cmdImpl = ctor.Invoke(new object[] { });
                    cmdMap.Add(cmdAttribute.name, (Cmd)cmdImpl);
                }
            }

            return new CmdMapper(cmdMap);
        }
    }
}
