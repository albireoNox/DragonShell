using System.Collections.Generic;

namespace game_system_rulesets.objects
{
    public class Ruleset
    {
        public readonly RulesetInfo info;
        public readonly List<EntityType> entityTypes;

        internal Ruleset(RulesetInfo info, List<EntityType> entityTypes)
        {
            this.info = info;
            this.entityTypes = entityTypes;
        }
    }
}
