using System;
using cli_application.app;
using game_engine;
using Attribute = game_engine.game_objects.Attribute;

namespace cli_application.cmd.commands
{
    [Cmd("get")]
    public class Get : Cmd
    {
        public override void executeCmd(string args, Game game, Application app)
        {
            if (string.IsNullOrWhiteSpace(args))
            {
                throw new ArgumentException("Must provide attribute name");
            }

            string[] keys = args.Trim().Split(".");

            if (keys.Length != 2)
            {
                throw new ArgumentException(
                    "The only argument syntax currently supported is <entityName>.<attributeName>");
            }

            string entityKey = keys[0].Trim().ToUpperInvariant();
            string attributeKey = keys[1].Trim().ToUpperInvariant();

            Attribute attribute = game.entities.findAttribute(entityKey, new[] {attributeKey});

            if (attribute == null)
            {
                throw new ApplicationException($"Could not find attribute with key '{args}'");
            }

            app.textOut.writeLine(attribute.ToString());
        }

        public override string getHelpText()
        {
            return string.Join(Environment.NewLine,
                "DESCRIPTION",
                "\tGets the value of a particular stat or attribute from the named entity",
                "USAGE",
                "\tget [entity name].[stat name]",
                "EXAMPLES",
                "\tget myPc.dex"
            );
        }
    }
}
