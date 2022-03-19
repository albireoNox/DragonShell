using System;
using System.Collections.Generic;

namespace game_engine.game_objects
{
    public class EntityBag
    {
        private readonly Dictionary<string, Entity> entities;

        public EntityBag()
        {
            this.entities = new Dictionary<string, Entity>();
        }

        public Entity getEntity(string key)
        {
            return this.entities.GetValueOrDefault(key);
        }

        public void addEntity(Entity entity, IEnumerable<string> aliases = null)
        {
            this.entities.Add(entity.name, entity);
            if (aliases != null)
            {
                foreach (var alias in aliases)
                {
                    this.entities.Add(alias, entity);   
                }
            }
        }

        public virtual Attribute findAttribute(string rootEntityKey, IList<string> keys)
        {
            if (keys.Count != 1)
            {
                throw new NotImplementedException("Multi-level attribute lookup not yet implemented");
            }

            Entity rootEntity = getEntity(rootEntityKey);
            if (rootEntity == null)
            {
                return null;
            }

            return rootEntity.getAttribute(keys[0]);
        }
    }
}
