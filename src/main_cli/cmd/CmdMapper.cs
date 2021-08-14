using System;
using System.Collections.Generic;
using System.Reflection;
using main_cli.app;

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

        public static CmdMapper createWithMappings(Singletons singletons)
        {
            var cmdMap = new Dictionary<string, Cmd>();

            var a = Assembly.GetExecutingAssembly();
            foreach (var type in a.GetTypes())
            {
                var attributes = type.GetCustomAttributes(typeof(CmdAttribute), false);
                if (attributes.Length > 0)
                {
                    CmdAttribute cmdAttribute = (CmdAttribute)attributes[0];
                    ConstructorInfo ctor = type.GetConstructor(new[] { typeof(Singletons) });
                    if (ctor == null)
                    {
                        throw new ApplicationException("Could not find public constructor for type " + type.FullName);
                    }
                    var cmdImpl = ctor.Invoke(new object[] { singletons });
                    cmdMap.Add(cmdAttribute.name, (Cmd)cmdImpl);
                }
            }

            return new CmdMapper(cmdMap);
        }
    }
}
