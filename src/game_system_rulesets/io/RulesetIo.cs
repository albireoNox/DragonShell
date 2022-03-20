using System.Collections.Generic;
using System.IO;
using common.io;
using game_system_rulesets.objects;

namespace game_system_rulesets.io
{
    public static class RulesetIo
    {
        private static readonly string RULESET_DIR = "ruleset_data";
        private static readonly string ENTITIES_DIR = "entities";
        private static readonly string RULESET_MANIFEST_NAME = "system.yaml";

        public static Ruleset loadRuleset(string gameSystemId)
        {
            DirectoryInfo curDir = FileSystem.currentDirectory();
            DirectoryInfo rulesetRootDir = curDir.getSubdirectory(RULESET_DIR);

            foreach (var rulesetDir in rulesetRootDir.GetDirectories())
            {
                // Search for system file
                FileInfo manifestFile = rulesetDir.getFile(RULESET_MANIFEST_NAME);

                RulesetInfo rulesetInfo = YamlFile.deserializeYamlFile<RulesetInfo>(manifestFile);
                if (rulesetInfo.Id.Equals(gameSystemId))
                {
                    return buildRuleset(rulesetInfo, rulesetDir);
                }
            }

            throw new IOException($"Could not find rule set for game system '{gameSystemId}'");
        }

        private static Ruleset buildRuleset(RulesetInfo info, DirectoryInfo root)
        {
            DirectoryInfo entityDir = root.getSubdirectory(ENTITIES_DIR);

            List<EntityType> entities = new List<EntityType>();
            foreach (var entityDefFile in entityDir.EnumerateFiles("*.yaml"))
            {
                entities.Add(YamlFile.deserializeYamlFile<EntityType>(entityDefFile));
            }

            return new Ruleset(info, entities);
        }
    }
}
