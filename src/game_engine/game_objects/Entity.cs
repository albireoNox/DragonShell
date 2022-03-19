using System.Collections.Generic;

namespace game_engine.game_objects
{
    public class Entity
    {
        public readonly string name;
        public readonly string type;
        private readonly Dictionary<string, Attribute> attributes;

        public Entity(string name, string type)
        {
            this.name = name;
            this.type = type;
            this.attributes = new Dictionary<string, Attribute>();
        }

        public Attribute getAttribute(string key)
        {
            return this.attributes.GetValueOrDefault(key);
        }

        public void addAttribute(Attribute attribute, IEnumerable<string> aliases = null)
        {
            attributes.Add(attribute.name, attribute);
            if (aliases != null)
            {
                foreach (var alias in aliases) {
                    attributes.Add(alias, attribute);
                }
            }
        }
    }
}
